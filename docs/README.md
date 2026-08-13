# Decision Foundry Core documentation

Decision Foundry Core is the public reusable engine for the canonical 72-seat Decision Foundry institution.

## Architecture

- [Core runtime contract](architecture/core-runtime-contract.md) — the 72-seat institution, 12-of-25 Director cohort, provider-neutral connector boundary and minimal observability/sample contract.
- [Canonical seat profile contract](architecture/seat-profile-contract.md) — executable role semantics used to staff all 72 seats and preserve the distinct reason each seat exists.
- [Recruiter contract](architecture/recruiter-contract.md) — two-stage `best person for the job` mechanism: staff all 72 seats from eligible occupants, then select 12 of the 25 staffed Directors for a work request.

## Recruiter boundary

The public Recruiter is a composition capability, not a 73rd canonical Core seat.

It does not require its own model connection merely to operate. In the trivial single-candidate case, the same occupant may fill all 72 seats deterministically. When multiple occupants/models are available, the Recruiter ranks their affinity against the canonical seat profiles.

Product-specific candidate sourcing, identity, user weighting and manual team selection remain outside Core.

## Documentation boundary

This public repository documents only contracts and behavior that belong to the reusable Core engine.

Product-specific tenancy, identity, candidate-source semantics, private weighting rules, manual team-selection policies, private orchestration participants and commercial product strategy belong to the products that consume Core and are intentionally not specified here.
