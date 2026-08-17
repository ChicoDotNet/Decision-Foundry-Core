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

S01-S07 materialize D01-D25 and close the Director layer at **25 / 25 Directors and 2,500 statements**.

### S08-S10 — all 12 transversals

S08-S10 materialize T01-T12 while preserving distinct platform authorities and close the transversal layer at **12 / 12 transversals and 3,700 cumulative statements**.

### S11-S12 — executive integration and flow

S11-S12 materialize VP01-VP05, S00 Strategist and CDO, then begin the Elder layer. VP inputs require Council III reconciliation; VP outputs require Council IV approval before Strategist integration. The CDO observes flow and escalates blockers without issuing substantive opinions or votes.

### S13-S17 — Councils I-III

S13-S14 complete **Council I Validation 7 / 7** and start Council II. S15-S16 complete **Council II Certification 7 / 7** and start Council III. S17 completes **Council III Reconciliation 7 / 7**.

### S18 — Council IV Approval start

S18 materializes the first four Council IV Elders:

- **ELD-APP-TRUTH** evaluates executive claim accuracy, traceability/freshness, material assumptions and unknowns, contradictions/dissent, quantitative/causal integrity, conditions and readiness claims.
- **ELD-APP-WISDOM** evaluates evidence-weighted executive judgment, strategic trade-offs, alternatives and opportunity cost, second-order consequences, enterprise fit, uncertainty and escalation judgment.
- **ELD-APP-JUSTICE** evaluates affected parties, rights and obligations, benefit/burden distribution, procedural fairness and voice, conflicts of interest, power asymmetry, remedies and recourse.
- **ELD-APP-MERCY** evaluates foreseeable harm, vulnerability, necessity and proportionality, lower-harm alternatives, human burden, accountability without excess harm, remediation and recovery.

Council IV reviews the **Vice President → Strategist** transition and asks whether the recommendation deserves to reach the Strategist as an approved proposal. Each Elder issues only its assigned criterion vote. Only the collective Council IV process grants executive approval. Conditions, dissent, contradictions and invalidation conditions remain visible after progression. Irreversible or materially legal, financial, security, privacy, continuity or public action must be escalated as `ReadyForHumanDecision`; no individual Elder or the Strategist may substitute for the Human Decision Owner.

After S18 the catalog contains **69 / 72 complete scorecards and 6,900 / 7,200 canonical statements**. The Elder layer stands at **25 / 28**. Council I Validation, Council II Certification and Council III Reconciliation remain **7 / 7 complete**; Council IV Approval stands at **4 / 7**.

## Validation

Run:

```bash
python3 tools/validate_scorecards.py
```

During partial construction the manifest validates the materialized subset and declared counts. When `complete` becomes `true`, the validator additionally requires exactly 72 seat files and exactly 7,200 statements.

### Accepted increments

- **S15:** functional success on `8215567acd6d7da473b780a8ffd20a630588dcde`; final acceptance-marker exact head `f4d313149400f1966df8a653bc626f7ff81040ee` also passed.
- **S16:** functional success on `0bc2e89475b2800555bff3a9279825241cb6c817`; final acceptance-marker exact head `74f7d917d02fa3e0e03ba7919905af5295fd9c20` also passed.
- **S17:** functional success on `60f7374b95fbe505d34084cc230586ff3271861e`; final exact head `443c77442729264bcd0bf2fff38cd6bbfa425b0a` also passed.
- **S18:** functional success on `2e8e9d208ad962e690cb2415e009f47b4b2eebf6`; final exact-head marker is accepted only after this documentation head also passes `Validate seat scorecards`.
