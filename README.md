# Decision Foundry Core

Decision Foundry Core is the reusable open-source .NET engine behind the canonical **72-seat Decision Foundry institution**.

It is designed so someone can understand and use the engine without knowing any private Enterprise strategy: provide eligible AI/model connectors, optionally bind specific connectors to specific seats, let the Recruiter staff the institution, create an Instrumentation Plan for the work, recruit 12 of the 25 staffed Directors, and execute through provider-neutral contracts.

## The shortest complete mental model

```text
Work request
    |
    v
Instrumentation Plan
  - decisive questions
  - artifacts / deliverables
  - lineage-lens references
  - evidence requirements
  - required expertise
    |
    v
72 canonical seat profiles
      x
eligible concrete AI/model candidates
    |
    v
72 occupied seats
    |
    v
25 occupied Directors
      x
current Instrumentation Plan
    |
    v
12 recruited Directors
    |
    v
bounded institutional execution
```

Core is **not** 72 simultaneous model calls. The 72 are logical institutional jobs.

## Four concepts to understand first

### Seat = the job

Core defines **72 canonical seats**:

- 1 Strategist;
- 5 Vice Presidents;
- 25 Directors;
- 12 transversal production/platform roles;
- 28 Elders; and
- 1 Chief Delivery Officer / flow role.

Every seat has a stable identity and an executable role profile describing why that job exists.

### Occupant candidate = the concrete AI/model connection

A candidate is a concrete selectable connector/model/version/deployment identity supplied by the host.

The same candidate may occupy many or all 72 seats. Different concrete versions from one provider may occupy different seats.

A host may use:

- fully automatic staffing;
- fully explicit seat binding; or
- mixed staffing where selected seats are pinned and the Recruiter fills the rest.

### SeatAffinity = deterministic fit of candidate to job

Every canonical seat owns an ideal scorecard of exactly **100 positive statements**.

For each candidate, the host/connector supplies one `0..100` agreement percentage per statement. Version 1 affinity is simply:

```text
SeatAffinity = arithmetic mean of the 100 responses
```

The 100-answer vector and scorecard version are evidence. Candidate eligibility/default/cost policy is separate and must never alter the stored affinity percentage.

### Instrumentation Plan = what work this case actually needs

The [Instrumentation Plan contract](docs/architecture/instrumentation-plan-contract.md) connects the work request to:

- decisive questions;
- selected artifact/deliverable references;
- useful lineage-lens references;
- evidence requirements;
- required expertise;
- dependencies and quality gates; and
- the staffed Directors recruited to perform that work.

It prevents the engine from asking every Director to participate in every case or selecting Directors by title matching alone.

## What the Recruiter does

The Recruiter has two distinct responsibilities.

### Stage 1 — staff the whole institution

For every unbound seat, Core compares the eligible candidates against the same version of that seat's 100-statement scorecard and assigns the highest-affinity candidate.

A single user/community host can expose one connection and legitimately use it for all 72 seats. If several concrete models are available, different models may win different seats.

### Stage 2 — recruit 12 of the 25 staffed Directors for the work

For a concrete work request, Core evaluates only the **25 occupied Director seats D01-D25** against the Instrumentation Plan and selects exactly **12 distinct Director seats with their current occupants**.

The other institutional roles do not compete for those 12 Director positions. Governance, synthesis, transversal and flow roles activate according to their own contracts.

Twelve recruited Directors also does not imply twelve concurrent requests. Scheduling remains bounded and dependency-aware.

## Candidate eligibility and explicit choice

Core lets a host keep **eligibility/default-selection policy** separate from job affinity.

A host may mark candidates eligible/ineligible, designate a preferred/default candidate, provide optional normalized expected-cost metadata and apply a generic ceiling. Those values decide **who may compete or be selected**; they do not add or subtract points from `SeatAffinity`.

A host can explicitly bind any eligible concrete candidate to a seat. The binding preserves the exact connector/model/version/deployment identity and whether the choice was automatic or explicit.

See the [candidate eligibility](docs/architecture/candidate-eligibility-contract.md) and [explicit seat binding](docs/architecture/seat-binding-contract.md) contracts.

## Public/private boundary

Core intentionally does not know:

- Enterprise users or tenants;
- social contacts;
- private candidate-source rules;
- private usage/spend history;
- private reassessment cadence;
- private manual-team semantics; or
- commercial product strategy.

Consuming products supply those concerns through generic host extension points.

## Reference sample

The first release should include a deliberately small **Console sample** that proves the DLL rather than becoming another product.

It should show:

- the 72 canonical seats;
- their concrete occupants;
- automatic vs explicit binding provenance;
- the current Instrumentation Plan summary;
- the 12 recruited Directors;
- required non-Director activations; and
- observable state such as `Idle`, `Working` and `Completed`.

A WinForms/XAML monitor may be added later without changing the Core contract.

## Read the documentation in order

If you are new to Core, start with the **[Guided Reading Path](docs/reading-guide.md)** rather than browsing `docs/` randomly.

The shorter architecture index is [docs/README.md](docs/README.md).

## License

Public releases in this repository are licensed under **GNU Affero General Public License v3.0 only (`AGPL-3.0-only`)**.

Copyright © 2026 Alfonso Lara Ramos.

A separate license grant for ASBN is recorded in [`NOTICE`](NOTICE). That separate grant is not extended to the public.

Before accepting external contributions, this project will adopt contribution terms that preserve the copyright holder's ability to maintain the public AGPL license and separate commercial licensing.

## Current status

**Foundation / architecture contract only.** No complete Decision Foundry runtime implementation has been promoted to Core yet.

The approved first implementation shape is a reusable .NET DLL with:

- stable 72-seat catalog/profiles;
- 100-statement scorecards;
- provider-neutral occupant/connectors;
- candidate eligibility and explicit/mixed seat binding;
- institutional staffing;
- Instrumentation Plan contract;
- 12-of-25 Director recruitment;
- bounded orchestration; and
- observable execution state.

Implementation must arrive through reviewed, tested increments rather than by copying private product code wholesale.