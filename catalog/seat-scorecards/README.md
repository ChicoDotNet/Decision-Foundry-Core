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

S08-S10 materialize T01-T12 while preserving distinct platform authorities. S10 closes the transversal layer at **12 / 12 transversals and 3,700 cumulative statements**.

### S11-S12 — executive integration and flow

S11 materializes VP01-VP04. S12 completes VP05, adds S00 Strategist and CDO, and begins the Elder layer with ELD-VAL-TRUTH. VP inputs require Council III reconciliation; VP outputs require Council IV approval before Strategist integration. The CDO observes flow and escalates blockers without issuing substantive opinions or votes.

### S13-S14 — Council I completion and Council II start

S13 adds Validation Elders for Wisdom, Justice, Mercy and Prudence. S14 adds Validation Stewardship and Purpose, closing **Council I Validation at 7 / 7**, then begins Council II with Certification Truth and Wisdom.

### S15-S16 — Council II completion and Council III start

S15 adds Certification Elders for Justice, Mercy, Prudence and Stewardship. S16 adds Certification Purpose, closing **Council II Certification at 7 / 7**, and begins Council III with Reconciliation Truth, Wisdom and Justice.

### S17 — Council III Reconciliation complete

S17 materializes the four remaining Reconciliation Elders:

- **ELD-REC-MERCY** evaluates cross-domain harm and burden, vulnerability, accountability without unnecessary harm, proportionality, lower-harm alternatives, dissent about harm and remediation burden.
- **ELD-REC-PRUDENCE** evaluates timing, reversibility, foreseeable consequences, uncertainty triggers, sequencing, dependencies, downside paths and decision pace.
- **ELD-REC-STEWARDSHIP** evaluates financial commitments, time and attention, authority, capacity, resource constraints, enterprise waste, sustainability and accountability for resource use.
- **ELD-REC-PURPOSE** evaluates mission alignment, intended-outcome coherence, purpose evidence, mission drift, stakeholder/enterprise purpose, outcome measures and purpose invalidation conditions.

Council III asks whether a **Director → Vice President** recommendation can coexist with enterprise constraints and other domains. Each Elder issues only its assigned criterion vote. Reconciliation preserves dissent, contradictions and invalidation conditions; it does not rewrite the Director recommendation, manufacture consensus, emit the collective Council result from one criterion, approve the business decision, or replace Council IV executive approval.

After S17 the catalog contains **65 / 72 complete scorecards and 6,500 / 7,200 canonical statements**. The Elder layer stands at **21 / 28**. Council I Validation is **7 / 7**, Council II Certification is **7 / 7**, and Council III Reconciliation is **7 / 7**.

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
- **S12:** corrected functional head `7e99ea342aa07683e9346907d5516b07ddabf040` passed after fixing manifest ordering; final exact head `9ed93b4014fbe2ca992ec56ac44396c34500801e` also passed.
- **S13:** functional success on `2f0e2a758c8af94ff176709d35cf162515de0b60`; acceptance-marker exact head `0e35f404beb3f64b9a814fca1cfabc65089ac0be` also passed.
- **S14:** final accepted head recorded in the S15 predecessor history.
- **S15:** functional success on `8215567acd6d7da473b780a8ffd20a630588dcde`; final acceptance-marker exact head `f4d313149400f1966df8a653bc626f7ff81040ee` also passed.
- **S16:** functional success on `0bc2e89475b2800555bff3a9279825241cb6c817`; final acceptance-marker exact head `74f7d917d02fa3e0e03ba7919905af5295fd9c20` also passed.
- **S17:** accepted only after the exact functional head containing the four new Reconciliation Elder scorecards, the `1.0.0-dev.17` manifest and this documentation passes `Validate seat scorecards`.
