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

`v1/D07.json` remains the golden reference, projected from the validated 100-subject ASBN CaOS Marketing Specialist professional doctorate.

### Seat-contract dimensional projection

A seat whose 100-subject professional doctorate has not yet been materialized must not fabricate curriculum provenance.

- Directors: `canonical seat contract + decisive questions -> 10 professional criterion dimensions -> 10 positive statements per dimension`.
- Transversals: `canonical transversal contract + outputs + quality focus + permanent skill boundaries -> 10 dimensions -> 10 statements per dimension`.
- Vice Presidents: `canonical VP mission + reconciled Director portfolio + quality focus + governance transitions + prohibitions -> 10 executive integration dimensions -> 10 statements per dimension`.
- Strategist: `canonical S00 mission + five Council-IV-approved VP opinions + enterprise trade-offs + Human Decision Owner boundary -> 10 executive integration dimensions -> 10 statements per dimension`.
- Chief Delivery Officer: `canonical flow mission + procedural authority + standard outputs + metrics + prohibitions -> 10 institutional-flow dimensions -> 10 statements per dimension`.
- Elders: `governance stage mission + criterion focus + stage quality + vote/conditions/escalation + prohibitions -> 10 governance-evaluation dimensions -> 10 statements per dimension`.

These scorecards measure the authority and competence of the canonical seat, not a larger generic profession. A later doctorate may justify a new scorecard version and reassessment, but must not silently rewrite historical v1 evidence.

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

S01-S07 materialize D01-D25. D07 is the golden doctorate-backed projection; the remaining Directors use the approved contract-dimensional model. S07 closes the Director layer at **25 / 25 Directors and 2,500 statements**.

### S08-S10 — all 12 transversals

S08-S10 materialize T01-T12. Artifact selection, evidence management, question generation, research/retrieval, quantitative analysis, artifact generation/validation, contradiction detection, executive compression, context packaging, citation/provenance checking and quality gating remain distinct. S10 closes the transversal layer at **12 / 12 transversals and 3,700 cumulative statements**.

### S11 — VP01, VP02, VP03, VP04

VP01-VP04 integrate their five reconciled Director portfolios without duplicating Director domain ownership. All VP inputs must already have passed Council III reconciliation. VP synthesis preserves governance conditions, evidence, uncertainty, contradiction and dissent; the VP opinion must pass Council IV before Strategist integration.

### S12 — VP05, S00, CDO, ELD-VAL-TRUTH

- **VP05 Evidence, Contradiction and Integration** integrates D21-D25 while protecting provenance, falsification, impact/ethics, expert comparability, dissent and benefit realization. It completes the **5 / 5 Vice President layer**.
- **S00 Strategist** integrates only Council-IV-approved VP opinions, resolves enterprise-level trade-offs, selects priorities and produces the final decision opinion while preserving dissent, conditions and material uncertainty. It does not approve irreversible or materially sensitive action for the Human Decision Owner.
- **CDO Chief Delivery Officer** protects institutional flow through visibility and escalation. It detects blockers, missing owners, dependency loops, aging work, forgotten dissent and coordination overhead, but does not issue domain/moral/strategic opinions, vote in Councils, mutate evidence or approve decisions.
- **ELD-VAL-TRUTH Validation Elder — Truth** evaluates whether specialized work is valid for Manager synthesis through the Truth criterion: evidence and claims must be accurate, traceable and not misleading. It issues one criterion-specific Council I vote with rationale and conditions without replacing or rewriting production analysis.

The S12 boundary is intentionally non-collapsing: **VP05 integrates evidence-related Directors; S00 integrates approved VP opinions; CDO only governs procedural flow; ELD-VAL-TRUTH only evaluates Truth at Council I.** None inherits the authority of another seat.

After S12 the catalog contains **45 / 72 complete scorecards and 4,500 / 7,200 canonical statements**, with **25 / 25 Directors, 12 / 12 transversals and 5 / 5 Vice Presidents complete**. The Elder layer has begun at **1 / 28**.

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
- **S09:** functional success on `7ac13b8fbb091fb5ea43ac5576023b4f3587cfbf`; final documentation head `d164e2c223ca0df229bb61b1b5e466bd70df326f` also passed.
- **S10:** functional success on `950315558c28eca88cf5f09980aff3cdef8ef088`; final exact-head documentation passed the same workflow.
- **S11:** functional success on `3165ab7bd28c5c303c3b62d21aa92afb2857d154`; final exact head `582ddf0bc5cf570cd8ecc1a242e3601149cfd139` passed the same workflow.
- **S12:** accepted only after the exact functional head containing VP05, S00, CDO, ELD-VAL-TRUTH, the `1.0.0-dev.12` manifest and this documentation passes `Validate seat scorecards`.
