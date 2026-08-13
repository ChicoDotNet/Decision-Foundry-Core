# Decision Foundry Core

Decision Foundry Core is the reusable open-source .NET engine for the canonical **72-seat Decision Foundry runtime**.

The intended public experience is deliberately small: reference the DLL, provide one or more AI/model connectors, and let the runtime execute the 72 logical agent seats through a provider-neutral boundary.

## Public runtime model

Core is designed to support a very small deployment as well as richer hosts:

- one user may configure a **single AI/model connection and bind it to all 72 seats**;
- groups of seats may share connectors; or
- every seat may receive its own connector.

The 72 seats remain logically distinct even when the same underlying model/provider services all of them. Core does not require 72 accounts, 72 providers or 72 human users.

Core also does **not** require an enabled Recruiter, tenant management or private multi-user weighting logic. Product-specific hosts may extend the composition without changing the public 72-seat contract.

Core may optionally expose a reusable **Recruiter** capability that ranks connector/model candidates by task affinity. A single connection can expose multiple models, and hosts with multiple candidates can opt into periodic reweighting through a configurable policy. The Recruiter is not a 73rd Core seat and the public contract does not know product-specific users, contacts, teams or private weighting semantics.

See the [Core runtime contract](docs/architecture/core-runtime-contract.md) and [Recruiter contract](docs/architecture/recruiter-contract.md) for the approved architecture direction.

## Reference sample

Core will include at least one intentionally small sample host — Console, WinForms or a XAML-based desktop application — whose only job is to prove the DLL contract and show the state of all 72 seats.

At minimum the sample will indicate whether each seat is:

- `Idle`
- `Working`
- `Completed`

If the optional Recruiter is enabled, its state is shown separately from the 72 canonical seats.

The sample is not intended to become a second product.

## Product-family role

The public Core repository contains only reusable contracts and implementation that can stand independently of private product infrastructure.

Reusable capabilities may be promoted from production-oriented product work when they are independently testable, safe to publish and not product-specific. Private products consume or extend Core; Core does not become a mirror of them and does not need to know their private strategy.

## License

Public releases in this repository are licensed under **GNU Affero General Public License v3.0 only (`AGPL-3.0-only`)**.

Copyright © 2026 Alfonso Lara Ramos.

A separate license grant for ASBN is recorded in [`NOTICE`](NOTICE). That separate grant is not extended to the public.

Before accepting external contributions, this project will adopt contribution terms that preserve the copyright holder's ability to maintain the public AGPL license and separate commercial licensing.

## Current status

**Foundation / architecture contract only.** No Decision Foundry implementation has been promoted to Core yet.

The approved next shape is a reusable DLL exposing the 72-seat runtime, replaceable connector resolution, optional generic recruitment and observable execution state. Implementation will arrive through reviewed increments rather than by copying the private product wholesale.
