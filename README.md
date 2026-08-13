# Decision Foundry Core

Decision Foundry Core is the reusable open-source engine that will be promoted selectively from the private Decision Foundry product as reusable contracts and implementation prove themselves in production-oriented Enterprise work.

## Product-family role

The intended sequence is deliberate:

1. **Decision Foundry Enterprise first** — build and validate the private enterprise product and its real operating boundaries.
2. **Promote reusable capabilities to Core** — move only code that is demonstrably reusable, independently testable, safe to publish and not product-specific.
3. **Foundry Commons second** — use Core as the shared engine for the future social simulation product.
4. **Foundry Commons as proof and distribution** — the social product can publicly demonstrate the engine that organizations can adopt privately through Decision Foundry Enterprise.

This repository must not become a mirror of the private Decision Foundry repository. Promotion to Core is an explicit architectural and security decision.

## License

Public releases in this repository are licensed under **GNU Affero General Public License v3.0 only (`AGPL-3.0-only`)**.

Copyright © 2026 Alfonso Lara Ramos.

A separate license grant for ASBN is recorded in [`NOTICE`](NOTICE). That separate grant is not extended to the public.

Before accepting external contributions, this project will adopt contribution terms that preserve the copyright holder's ability to maintain the public AGPL license and separate commercial licensing.

## Current status

**Foundation only.** No Decision Foundry implementation has been promoted to Core yet.

The private product remains the place where Enterprise architecture is designed and validated. Core will receive capabilities later through reviewed, traceable promotion increments.
