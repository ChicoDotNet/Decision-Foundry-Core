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

## Authoring models

### Professional-doctorate projection

For a seat backed by a mature 100-subject professional doctorate:

`Subject N -> exit competence / mastery criterion -> positive occupant criterion -> Statement N`

`v1/D07.json` is the golden reference, projected from the validated 100-subject ASBN CaOS Marketing Specialist professional doctorate.

### Seat-contract dimensional projection

A seat whose 100-subject professional doctorate has not yet been materialized must not fabricate curriculum provenance.

For Directors with a decisive-question catalog, the pattern is:

`canonical seat contract + decisive questions -> 10 professional criterion dimensions -> 10 positive statements per dimension`

For transversal seats, which do not have a seat-specific decisive-question catalog, the pattern is:

`canonical transversal contract + outputs + quality focus + permanent skill boundaries -> 10 professional criterion dimensions -> 10 positive statements per dimension`

The resulting statements are real versioned v1 criteria, not placeholders. A later doctorate may justify a new scorecard version and reassessment, but must not silently rewrite historical v1 evidence.

## Statement completion plan

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

Every increment must leave complete 100-statement scorecards; partially authored seats do not count as progress.

## Materialized increments

### S01-S07 — all 25 Directors

S01-S07 materialize D01-D25. D07 is the golden doctorate-backed projection; the remaining Directors use the approved contract-dimensional model. S07 closes the complete Director layer at **25 / 25 Directors and 2,500 statements**.

### S08 — T01, T02, T03, T04

- **T01 Artifact Selector** chooses artifact work from decision need, applicability, uncertainty reduction, risk coverage, actionability, dependencies, effort/value, evidence readiness, portfolio coherence, and selection governance.
- **T02 Evidence Manager** governs evidence identity, provenance, versions, deduplication, claim links, authorization, freshness, invalidation, contradictions, and handoff.
- **T03 Question Generator** turns decision uncertainty into bounded decisive questions with coverage, falsifiability, sequencing, adaptive questioning, and gap detection.
- **T04 Research and Retrieval** retrieves authorized internal and external evidence with scoped search, provenance, freshness, source quality, triangulation, and reproducible handoff.

Their boundaries remain distinct: T01 chooses work; T02 governs acquired evidence; T03 defines what must be answered; T04 retrieves authorized evidence.

### S09 — T05, T06, T07, T08

- **T05 Quantitative Analysis** — problem formulation, data fitness, reproducible calculation, model assumptions, statistical uncertainty, sensitivity/scenarios, numerical validation, interpretation limits, audit trail, and governed quantitative handoff.
- **T06 Artifact Generator** — registry-contract interpretation, input readiness, schema conformance, evidence-bounded claims, question coverage, quantitative/citation integration, dissent preservation, usability, reproducible generation, and authority-safe handoff.
- **T07 Artifact Validator** — contract resolution, schema, lineage, dependencies, question coverage, semantic quality, quantitative/citation checks, defect severity, regression/reproducibility, and validator authority boundaries.
- **T08 Contradiction Detector** — contradiction definition, normalized claims, assumption conflicts, metric conflicts, version/temporal conflicts, recommendation conflicts, alternative explanations, materiality, contradiction lifecycle, and dissent-preserving handoff.

The S09 chain is intentionally non-collapsing: **T05 analyzes; T06 generates; T07 validates without rewriting the artifact to make it pass; T08 detects and preserves contradictions without selecting the winner.** None of these roles gains domain, synthesis, certification, approval, or human-decision authority.

After S09 the catalog contains **33 / 72 complete scorecards and 3,300 / 7,200 canonical statements**, with **8 / 12 transversal seats complete**.

## Validation

Run:

```bash
python3 tools/validate_scorecards.py
```

During partial construction the manifest validates the materialized subset and declared counts. When `complete` becomes `true`, the validator additionally requires exactly 72 seat files and exactly 7,200 statements.

### Accepted increments

- **S01:** success on `4d130e0db9fef425ce5752414ed4ee8b42378b88`.
- **S02:** success on `eddec91bcdf5aa8334e295d76f22b007c65ddbed`.
- **S03:** success on `f00e436556e6a2a3cedcf22d737d721a5bcddece` and subsequent metadata head.
- **S04:** functional success on `8b20e881bb4ea3f4a361dbd206686c70bb6954ed`; acceptance-marker head `a1a6e1abecc35ff0bf1cdde90180a773f03480b6` also passed.
- **S05:** functional success on `82f166cb8afa7a88e08d6f90a870e1f107833df2`; acceptance-marker head `4a97ed025100e2e9b707238e10d96ab5010c4b9f` also passed.
- **S06:** functional success on `ba5f01d67383be35881e9df9b4d7d33446aac4f6`; acceptance-marker head `271c459f53982f9d25bb11a4cd636a07148b588e` also passed.
- **S07:** functional success on `843bf3a96d102731bd8249b0c4fbc132b11410df`; subsequent metadata heads passed the same workflow.
- **S08:** functional success on `1e543c17b770ba938c54e491f529c3536a972e00`; final metadata head `9e219b3bec822f868ee749b883bdded75445970a` also passed.
- **S09:** functional success on `7ac13b8fbb091fb5ea43ac5576023b4f3587cfbf`; the acceptance-marker head must also pass the same exact-head workflow.
