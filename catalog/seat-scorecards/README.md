# Canonical seat scorecards

This directory owns the versioned public Decision Foundry Core catalog used to assess occupant affinity with each of the 72 canonical institutional seats.

## Invariants

- exactly 72 canonical seat scorecards in a complete catalog;
- exactly 100 positive statements per seat;
- exactly 7,200 statements per complete catalog version;
- stable statement identity inside one scorecard version;
- every statement describes desirable fit with the seat, never generic model prestige or personality;
- `100%` agreement always means stronger alignment;
- equal weighting in Scorecard v1;
- source/provenance is preserved so a future revision can explain why a statement exists.

## Professional-doctorate projection

For a seat backed by a mature 100-subject professional doctorate, the preferred authoring pattern is:

`Subject N -> exit competence / mastery criterion -> positive occupant criterion -> Statement N`

The mapping is deliberately close to one-to-one because the doctorate already decomposes professional mastery into 100 coherent learning areas. A statement is not copied from the subject title: it distills the competence, evidence discipline, failure boundaries and mastery expected from an excellent practitioner.

The mapping is a strong default, not an artificial rule for every seat. Transversal roles and Elders use their own canonical mission, authority, prohibitions, stage and criterion contracts when a 100-subject curriculum would be misleading.

## Statement completion plan

The scorecard program is intentionally 19 increments. S01 establishes the golden quality bar with D07 before higher-throughput batches begin.

| Increment | Seats completed | Increment statements | Cumulative |
|---|---|---:|---:|
| S01 | D07 | 100 | 100 |
| S02 | D04, D06, D15, D01 | 400 | 500 |
| S03 | D08, D10, D21, D02 | 400 | 900 |
| S04 | D03, D05, D25, D11 | 400 | 1,300 |
| S05 | D12, D13, D14, D16 | 400 | 1,700 |
| S06 | D17, D18, D20, D09 | 400 | 2,100 |
| S07 | D19, D22, D23, D24 | 400 | 2,500 |
| S08 | T01, T02, T03, T04 | 400 | 2,900 |
| S09 | T05, T06, T07, T08 | 400 | 3,300 |
| S10 | T09, T10, T11, T12 | 400 | 3,700 |
| S11 | VP01, VP02, VP03, VP04 | 400 | 4,100 |
| S12 | VP05, S00, CDO, ELD-VAL-TRUTH | 400 | 4,500 |
| S13 | ELD-VAL-WISDOM, ELD-VAL-JUSTICE, ELD-VAL-MERCY, ELD-VAL-PRUDENCE | 400 | 4,900 |
| S14 | ELD-VAL-STEWARDSHIP, ELD-VAL-PURPOSE, ELD-CERT-TRUTH, ELD-CERT-WISDOM | 400 | 5,300 |
| S15 | ELD-CERT-JUSTICE, ELD-CERT-MERCY, ELD-CERT-PRUDENCE, ELD-CERT-STEWARDSHIP | 400 | 5,700 |
| S16 | ELD-CERT-PURPOSE, ELD-REC-TRUTH, ELD-REC-WISDOM, ELD-REC-JUSTICE | 400 | 6,100 |
| S17 | ELD-REC-MERCY, ELD-REC-PRUDENCE, ELD-REC-STEWARDSHIP, ELD-REC-PURPOSE | 400 | 6,500 |
| S18 | ELD-APP-TRUTH, ELD-APP-WISDOM, ELD-APP-JUSTICE, ELD-APP-MERCY | 400 | 6,900 |
| S19 | ELD-APP-PRUDENCE, ELD-APP-STEWARDSHIP, ELD-APP-PURPOSE | 300 | 7,200 |

The ordering front-loads production roles, then transversal/platform work, then executive synthesis and finally the four governance stages. Every increment must leave complete 100-statement scorecards; partially authored seats do not count as progress.

## S01 golden scorecard

`v1/D07.json` is the first complete scorecard and the authoring reference for profession-backed seats.

Its primary curriculum provenance is the validated 100-subject **ASBN CaOS Marketing Specialist** professional doctorate. The D07 statements preserve D07's canonical mission — repeatable demand and revenue systems — while projecting the doctorate's scientific, buyer, commercial, creative, experience, measurement, AI, legal, financial, operational and doctoral standards into ideal-occupant criteria.

## Validation

Run:

```bash
python3 tools/validate_scorecards.py
```

During partial construction, the manifest validates the materialized subset and its declared counts. When `complete` becomes `true`, the validator additionally requires exactly 72 seat files and exactly 7,200 statements.
