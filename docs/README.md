# Decision Foundry Core documentation

Decision Foundry Core is the public reusable engine for the canonical 72-seat Decision Foundry institution.

## Architecture

- [Core runtime contract](architecture/core-runtime-contract.md) — the 72-seat institution, 12-of-25 Director cohort, provider-neutral connector boundary and minimal observability/sample contract.
- [Canonical seat profile contract](architecture/seat-profile-contract.md) — executable role semantics used to staff all 72 seats and preserve the distinct reason each seat exists.
- [Canonical 100-statement seat scorecard](architecture/seat-scorecard-contract.md) — exactly 100 positive statements per seat, 0–100 occupant responses from the bound user perspective and equal-weight arithmetic affinity scoring.
- [Candidate eligibility and preferred-model contract](architecture/candidate-eligibility-contract.md) — generic host-supplied default/preferred candidate, eligibility and optional normalized-cost ceiling hooks kept separate from job affinity.
- [Explicit seat binding contract](architecture/seat-binding-contract.md) — optional host/user choice of the concrete connector/model/version that occupies a seat, including mixed manual + automatic staffing.
- [Recruiter contract](architecture/recruiter-contract.md) — two-stage `best person for the job` mechanism: score and staff all unbound seats from eligible occupants, then select 12 of the 25 staffed Directors for a work request.

## Recruiter boundary

The public Recruiter is a composition capability, not a 73rd canonical Core seat.

It does not require its own model connection merely to operate. In the trivial single-candidate case, the same occupant may fill all 72 seats. When multiple occupant candidates are available, the host first supplies the eligible/default candidate set and Core then evaluates those candidates against the same version of the seat's 100-statement scorecard, selecting the highest arithmetic-mean affinity for every seat that is not explicitly bound.

A host may expose several concrete model versions/deployments from the same provider as independent candidates and bind them differently by seat. The staffing snapshot preserves the exact candidate/model/version, active eligibility-policy snapshot and whether the occupant was selected automatically or explicitly.

Default/preferred status and cost metadata never modify the 100-statement affinity percentage; they only affect candidate eligibility or UX defaults.

The complete 100-answer vector and scorecard version are evidence; a final percentage alone is not sufficient for reconstruction.

Product-specific candidate sourcing, identity, credentials, usage/spend policy, reevaluation cadence and manual team selection remain outside Core.

## Documentation boundary

This public repository documents only contracts and behavior that belong to the reusable Core engine.

Product-specific tenancy, identity, candidate-source semantics, manual team-selection policies, private orchestration participants and commercial product strategy belong to the products that consume Core and are intentionally not specified here.
