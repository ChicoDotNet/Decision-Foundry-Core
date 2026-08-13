# Decision Foundry Core documentation

Decision Foundry Core is the public reusable engine for the canonical 72-seat Decision Foundry runtime.

## Architecture

- [Core runtime contract](architecture/core-runtime-contract.md) — 72 seats, provider-neutral connector boundary, single-connector mode, extensibility and the minimal observability/sample contract.
- [Recruiter contract](architecture/recruiter-contract.md) — optional automatic recruitment by task affinity, generic candidate ranking/reweighting and the host boundary for candidate sources.

## Documentation boundary

This public repository documents only contracts and behavior that belong to the reusable Core engine.

Product-specific tenancy, identity, candidate-source semantics, private weighting rules, manual team-selection policies, private orchestration participants and commercial product strategy belong to the products that consume Core and are intentionally not specified here.
