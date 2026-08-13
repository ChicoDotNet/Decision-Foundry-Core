# Decision Foundry Core

Decision Foundry Core is the reusable open-source .NET engine for the canonical **72-seat Decision Foundry institution**.

The intended public experience is deliberately small: reference the DLL, provide one or more AI/model connectors, let the Recruiter staff the institution using a **best person for the job** policy, then execute work through the institutional flow.

## Public runtime model

Core represents 72 distinct canonical seats:

- 1 Strategist;
- 5 Vice Presidents;
- 25 Directors;
- 12 transversal production/platform roles;
- 28 Elders; and
- 1 Chief Delivery Officer / flow role.

Every seat has its own mission and execution contract. The 72 seats remain logically distinct even when the same underlying AI/model connection occupies many or all of them.

### Institutional staffing

The Recruiter evaluates eligible occupant/model candidates against the 72 canonical seat profiles and assigns the best available occupant to each seat.

A single user may configure one AI/model connection and legitimately use it to occupy all 72 seats. If that connection exposes multiple models, different models may be chosen for different seats.

The Recruiter does **not** require its own model connection merely to make the trivial single-candidate staffing decision.

### Work-specific Director recruitment

For a concrete work request, the Recruiter evaluates the **25 already occupied Directors D01-D25** and selects exactly **12 Directors** whose seat+occupant combinations have the strongest affinity for the assignment.

The other institutional roles do not compete for those 12 Director positions. Governance, synthesis, transversal and flow roles activate according to their own contracts.

The 12 selected Directors are not necessarily 12 simultaneous calls. Runtime scheduling remains bounded and dependency-aware.

See the [Core runtime contract](docs/architecture/core-runtime-contract.md), [canonical seat profile contract](docs/architecture/seat-profile-contract.md) and [Recruiter contract](docs/architecture/recruiter-contract.md).

## Reference sample

Core will include at least one intentionally small sample host — initially Console, with WinForms/XAML remaining possible later — whose only job is to prove the DLL contract.

The sample should show:

- all 72 canonical seats;
- the occupant/model assigned to each seat where appropriate;
- the 12 Directors recruited for the current work;
- which institutional roles become active as the flow advances; and
- execution state such as `Idle`, `Working` and `Completed`.

The sample is not intended to become a second product.

## Product-family role

The public Core repository contains only reusable contracts and implementation that can stand independently of private product infrastructure.

Core does not know Enterprise users, tenants, social contacts, private weighting formulas, credentials or manual-team semantics. Consuming products supply an authorized occupant candidate set and product-specific policy through public extension points.

## License

Public releases in this repository are licensed under **GNU Affero General Public License v3.0 only (`AGPL-3.0-only`)**.

Copyright © 2026 Alfonso Lara Ramos.

A separate license grant for ASBN is recorded in [`NOTICE`](NOTICE). That separate grant is not extended to the public.

Before accepting external contributions, this project will adopt contribution terms that preserve the copyright holder's ability to maintain the public AGPL license and separate commercial licensing.

## Current status

**Foundation / architecture contract only.** No Decision Foundry implementation has been promoted to Core yet.

The approved first implementation shape is a reusable DLL with the stable 72-seat catalog, provider-neutral occupant/connectors, institutional staffing, 12-of-25 Director recruitment, bounded orchestration and observable execution state.
