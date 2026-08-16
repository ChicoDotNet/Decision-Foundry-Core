#!/usr/bin/env python3
from __future__ import annotations

import json
from pathlib import Path
import sys

ROOT = Path(__file__).resolve().parents[1]
CATALOG = ROOT / "catalog" / "seat-scorecards" / "v1"
MANIFEST = CATALOG / "catalog.json"


def fail(message: str) -> None:
    raise ValueError(message)


def load(path: Path):
    with path.open("r", encoding="utf-8") as handle:
        return json.load(handle)


def validate_scorecard(path: Path) -> int:
    data = load(path)
    seat_id = data.get("seatId")
    if not isinstance(seat_id, str) or not seat_id:
        fail(f"{path}: seatId is required")

    statements = data.get("statements")
    if not isinstance(statements, list) or len(statements) != 100:
        fail(f"{path}: expected exactly 100 statements")

    if data.get("statementCount") != 100:
        fail(f"{path}: statementCount must equal 100")

    seen = set()
    for ordinal, item in enumerate(statements, start=1):
        expected_id = f"{seat_id}-S{ordinal:03d}"
        if item.get("id") != expected_id:
            fail(f"{path}: expected stable id {expected_id}")
        if expected_id in seen:
            fail(f"{path}: duplicate statement id {expected_id}")
        seen.add(expected_id)

        text = item.get("statement")
        if not isinstance(text, str) or not text.strip():
            fail(f"{path}: {expected_id} has no statement text")

        source_subject = item.get("sourceSubject")
        if source_subject is not None and source_subject != f"{ordinal:03d}":
            fail(
                f"{path}: {expected_id} sourceSubject must preserve ordinal "
                f"mapping to subject {ordinal:03d}"
            )

    provenance = data.get("provenance")
    if not isinstance(provenance, dict):
        fail(f"{path}: scorecard provenance is required")

    if data.get("authoringModel") == "professional-doctorate-projection":
        if provenance.get("curriculumSubjectCount") != 100:
            fail(f"{path}: doctorate-backed scorecards require 100 source subjects")
        for ordinal, item in enumerate(statements, start=1):
            if item.get("sourceSubject") != f"{ordinal:03d}":
                fail(
                    f"{path}: doctorate-backed {seat_id} statement {ordinal:03d} "
                    "must trace to the matching subject"
                )

    return len(statements)


def main() -> int:
    if not MANIFEST.exists():
        fail(f"Missing manifest: {MANIFEST}")

    manifest = load(MANIFEST)
    files = sorted(path for path in CATALOG.glob("*.json") if path.name != "catalog.json")
    declared_seats = manifest.get("materializedSeats")

    if not isinstance(declared_seats, list):
        fail("catalog.json: materializedSeats must be a list")

    actual_seats = []
    total_statements = 0
    for path in files:
        data = load(path)
        actual_seats.append(data.get("seatId"))
        total_statements += validate_scorecard(path)

    if actual_seats != declared_seats:
        fail(
            "catalog.json: materializedSeats must exactly match scorecard files "
            f"(declared={declared_seats}, actual={actual_seats})"
        )

    if manifest.get("materializedSeatCount") != len(files):
        fail("catalog.json: materializedSeatCount does not match scorecard files")

    if manifest.get("materializedStatementCount") != total_statements:
        fail("catalog.json: materializedStatementCount does not match statements")

    if manifest.get("statementsPerSeat") != 100:
        fail("catalog.json: statementsPerSeat must remain 100")

    if manifest.get("canonicalSeatCount") != 72:
        fail("catalog.json: canonicalSeatCount must remain 72")

    if manifest.get("expectedCompleteStatementCount") != 7200:
        fail("catalog.json: expectedCompleteStatementCount must remain 7200")

    if manifest.get("complete") is True:
        if len(files) != 72:
            fail("complete catalog must contain exactly 72 scorecards")
        if total_statements != 7200:
            fail("complete catalog must contain exactly 7,200 statements")

    print(
        f"Validated {len(files)} materialized seat scorecard(s), "
        f"{total_statements} statement(s); complete={manifest.get('complete')!r}."
    )
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except ValueError as exc:
        print(f"scorecard validation failed: {exc}", file=sys.stderr)
        raise SystemExit(1)
