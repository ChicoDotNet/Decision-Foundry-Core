# Decision Foundry Core documentation

Decision Foundry Core is the public reusable engine for the canonical 72-seat Decision Foundry institution.

## Architecture

- [Core runtime contract](architecture/core-runtime-contract.md) — the 72-seat institution, 12-of-25 Director cohort, provider-neutral connector boundary and minimal observability/sample contract.
- [Canonical seat profile contract](architecture/seat-profile-contract.md) — executable role semantics used to staff all 72 seats and preserve the distinct reason each seat exists.
- [Canonical 100-statement seat scorecard](architecture/seat-scorecard-contract.md) — exactly 100 positive statements per seat, 0–100 occupant responses from the bound user perspective and equal-weight arithmetic affinity scoring.
- [Recruiter contract](architecture/recruiter-contract.md) — two-stage `best person for the job` mechanism: score and staff all 72 seats from eligible occupants, then select 12 of the 25 staffed Directors for a work request.

## Recruiter boundary

The public Recruiter is a composition capability, not a 73rd canonical Core seat.

It does not require its own model connection merely to operate. In the trivial single-candidate case, the same occupant may fill all 72 seats. When multiple occupant candidates are available, Core evaluates each candidate against the same version of the seat's 100-statement scorecard and selects the highest arithmetic-mean affinity.

The complete 100-answer vector and scorecard version are evidence; a final percentage alone is not sufficient for reconstruction.

Product-specific candidate sourcing, identity, credentials, reevaluation cadence and manual team selection remain outside Core.

## Documentation boundary

This public repository documents only contracts and behavior that belong to the reusable Core engine.

Product-specific tenancy, identity, candidate-source semantics, manual team-selection policies, private orchestration participants and commercial product strategy belong to the products that consume Core and are intentionally not specified here.
