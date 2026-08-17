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

`v1/D07.json` is the golden reference for this model, projected from the validated 100-subject ASBN CaOS Marketing Specialist professional doctorate.

## Seat-contract dimensional projection

A seat whose 100-subject professional doctorate has not yet been materialized must not fabricate curriculum provenance merely to satisfy the scorecard count.

For those seats, the approved interim v1 authoring model is:

`canonical mission + responsibilities + outputs + quality focus + decisive questions -> 10 professional criterion dimensions -> 10 positive statements per dimension`

The resulting statements are real versioned assessment content, not placeholders. A later doctorate may justify a new scorecard version and reassessment, but must not silently rewrite historical v1 evidence.

The validator requires exactly 10 criterion dimensions, stable dimension identities, exactly 10 statements per dimension, no false `sourceSubject` provenance, explicit `professionalDoctorateStatus: not-yet-materialized`, and 100 unique positive `I ...` statements.

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

### S01 — D07 golden scorecard

Marketing, Sales, and Revenue: 100-subject doctorate projection preserving scientific, buyer, commercial, creative, experience, measurement, AI, legal, financial, operational, and doctoral standards.

### S02 — D01, D04, D06, D15

Problem/decision clarity; market/customer/competition; offer/product/pricing; and customer/operator/field reality.

### S03 — D02, D08, D10, D21

Corporate strategy; finance/treasury/value creation; scaling/profitability/independence; and case-file/evidence/traceability.

### S04 — D03, D05, D11, D25

Business model/purpose; investment/M&A/transferability; operations/processes; and integrated synthesis/benefits realization. D25 integrates evidence and benefits but does not replace the Strategist or Human Decision Owner.

### S05 — D12, D13, D14, D16

- **D12 Organization, People, and Adoption** — organization design, accountability, skills/capacity, incentives, change readiness, adoption behavior, leadership system, talent lifecycle, organizational evidence, and human-impact governance.
- **D13 Projects, Programs, and Transformation** — transformation framing, increments, dependencies/critical path, estimation, stage gates, risk/change control, delivery flow, readiness, benefits realization, and institutional learning.
- **D14 Governance, Performance, and Accountability** — minimal-effective governance, decision rights, accountable ownership, KPI architecture, management cadence, escalation/exceptions, commitment/benefit tracking, assurance, traceability, and governance adaptation.
- **D16 Enterprise Architecture and Microsoft** — current/target/transition architecture, architecture requirements, cloud/platform, application/integration, build-buy-partner-retain-retire, Microsoft interoperability, reliability/operability, lifecycle economics, and evolutionary governance.

D14 is explicitly optimized for **minimal effective governance** rather than bureaucratic volume. D16 treats Microsoft as a strong enterprise ecosystem to evaluate on evidence; vendor alignment is not itself an architectural objective.

After S05 the catalog contains **17 / 72 complete scorecards and 1,700 / 7,200 canonical statements**.

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
- **S05:** accepted only after the exact head containing all four S05 scorecards, `1.0.0-dev.5` manifest, and this documentation passes `Validate seat scorecards`.
