# Decision Foundry Core documentation

Decision Foundry Core is the public reusable engine for the canonical 72-seat Decision Foundry institution.

If you are learning the project, start with the **[Guided Reading Path](reading-guide.md)**. It provides one ordered route from the public mental model through seat profiles, scorecards, candidate eligibility, explicit bindings, the Recruiter, Instrumentation Plan and runtime boundaries.

## Architecture map

Read these contracts in this order:

1. [Core runtime contract](architecture/core-runtime-contract.md) — the 72-seat institution, 12-of-25 Director cohort, provider-neutral connector boundary and minimal observability/sample contract.
2. [Instrumentation Plan contract](architecture/instrumentation-plan-contract.md) — work-specific decisive questions, artifact/deliverable references, lineage-lens references, evidence requirements, expertise and Director recruitment inputs.
3. [Canonical seat profile contract](architecture/seat-profile-contract.md) — executable role semantics used to preserve the distinct reason each seat exists.
4. [Canonical 100-statement seat scorecard](architecture/seat-scorecard-contract.md) — exactly 100 positive statements per seat, `0..100` candidate responses and equal-weight arithmetic affinity scoring.
5. [Candidate eligibility and preferred-model contract](architecture/candidate-eligibility-contract.md) — generic host-supplied default/preferred candidate, eligibility and optional normalized-cost ceiling hooks kept separate from job affinity.
6. [Explicit seat binding contract](architecture/seat-binding-contract.md) — optional host/user choice of the concrete connector/model/version that occupies a seat, including mixed manual + automatic staffing.
7. [Recruiter contract](architecture/recruiter-contract.md) — two-stage `best person for the job`: staff all unbound seats from eligible occupants, then select 12 of the 25 staffed Directors for a work request.

## Core mental model

```text
Host-supplied eligible candidates
             |
             v
72 canonical seat profiles
             |
    100-statement affinity
    or explicit seat binding
             |
             v
       72 occupied seats
             |
             +--------------------------+
                                        |
                              Instrumentation Plan
                              questions + artifacts
                              lenses + evidence
                              required expertise
                                        |
                                        v
                              D01-D25 staffed Directors
                                        |
                                        v
                               12 recruited Directors
                                        |
                                        v
                              bounded institutional flow
```

## Recruiter boundary

The public Recruiter is a composition capability, not a 73rd canonical Core seat.

It does not require its own model connection merely to operate. In the trivial single-candidate case, the same occupant may fill all 72 seats. When multiple candidates are available, the host first provides the eligible/default candidate set and explicit bindings; Core then evaluates the remaining candidates against the same version of each seat's scorecard.

A host may expose several concrete model versions/deployments from the same provider as independent candidates and bind them differently by seat.

The complete 100-answer vector, scorecard version, active eligibility-policy snapshot, concrete candidate identity and automatic/explicit provenance are reconstruction evidence.

Default/preferred status and cost metadata never modify the 100-statement affinity percentage; they only affect eligibility or UX defaults.

## Instrumentation boundary

Core exposes a reusable `InstrumentationPlan` shape because work-specific Director recruitment needs to know **what professional work and expertise the current request requires**.

The public plan can carry stable references to artifacts/deliverables and lineage lenses without requiring Core to own private catalogs or proprietary content.

Lineage-lens references are metadata, not executable identities and not instructions to impersonate an external source.

## Documentation boundary

This public repository documents only contracts and behavior that belong to the reusable Core engine.

Product-specific tenancy, identity, candidate-source semantics, private reassessment schedules, manual team-selection policies, private pricing/usage calculations, private orchestration participants and commercial product strategy belong to consuming products and are intentionally not specified here.

## Documentation integrity rules

- Use [Guided Reading Path](reading-guide.md) as the canonical learning order.
- Keep **seat**, **occupant candidate**, **SeatAffinity**, **Instrumentation Plan** and **Director recruitment** as distinct concepts.
- Keep candidate eligibility/default policy separate from the canonical 100-statement affinity score.
- Keep explicit seat binding separate from 12-of-25 Director recruitment.
- Keep concurrency separate from seat count and recruited cohort size.
- Preserve stable/versioned identities and evidence needed to reconstruct completed runs.
- Do not introduce Enterprise user, tenant, social-contact or commercial semantics into public Core contracts.