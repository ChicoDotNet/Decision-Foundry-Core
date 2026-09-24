# Decision Foundry Core — Guided Reading Path

This is the canonical learning path for the public Decision Foundry Core repository.

Use it when you are a new user, contributor, reviewer, or AI agent and need to understand the reusable engine without reconstructing architecture from individual files.

## The one-sentence mental model

Decision Foundry Core is a provider-neutral .NET engine that represents a canonical **72-seat decision institution**, lets a host provide eligible AI/model occupants, staffs the institution using explicit bindings or deterministic `best person for the job` scorecards, plans the professional work needed for a case, and recruits **12 of the 25 staffed Directors** for that work.

```text
Work request
    |
    v
Instrumentation Plan
  questions + artifacts + lenses + required expertise
    |
    v
72-seat staffing snapshot
    |
    v
12-of-25 Director recruitment
    |
    v
bounded governed execution
```

Core does not define Enterprise users, tenants, social contacts, private budgets, private candidate-source rules or commercial product policy.

## Stage 1 — Start with the public product contract

1. [Repository README](../README.md) — shortest complete Core mental model and status.
2. [Core runtime contract](architecture/core-runtime-contract.md) — canonical 72-seat runtime and 12-of-25 work cohort.
3. [Instrumentation Plan contract](architecture/instrumentation-plan-contract.md) — how work requirements and expertise become one case-specific plan.

After this stage, you should understand that Core is not “72 simultaneous chats.” The seats are logical institutional jobs; active work is selected and scheduled deliberately.

## Stage 2 — Understand what a seat means

1. [Canonical seat profile contract](architecture/seat-profile-contract.md) — mission, outputs, authority, quality and other executable role semantics.
2. [Canonical 100-statement seat scorecard](architecture/seat-scorecard-contract.md) — exactly 100 positive statements per seat and deterministic equal-weight affinity.

A seat describes **the job**. It does not describe the current AI/model occupying it.

## Stage 3 — Understand occupant candidates

1. [Candidate eligibility and preferred-model contract](architecture/candidate-eligibility-contract.md) — host-provided eligibility/default/cost-policy metadata kept separate from affinity.
2. [Explicit seat binding contract](architecture/seat-binding-contract.md) — fully automatic, fully explicit or mixed staffing.
3. [Recruiter contract](architecture/recruiter-contract.md) — automatic institutional staffing and work-specific Director recruitment.

A concrete candidate can represent a specific provider/model/version/deployment. The same candidate may occupy many seats; different concrete versions may occupy different seats.

## Stage 4 — Understand the two Recruiter decisions

The Recruiter performs two distinct operations:

```text
Stage 1 — Institutional staffing
72 seat profiles × eligible candidates
-> 72 occupied seats

Stage 2 — Work-specific Director recruitment
25 occupied Directors × Instrumentation Plan/work request
-> exactly 12 recruited Directors
```

Do not conflate these decisions.

Non-Director institutional roles do not compete for one of the 12 Director slots. Governance, synthesis, transversal and flow roles activate according to their own contracts.

## Stage 5 — Understand instrumentation

Read [Instrumentation Plan contract](architecture/instrumentation-plan-contract.md) again after the Recruiter contract.

The important idea is that the runtime does not choose Directors by title matching alone. A plan should express:

- decisive questions;
- selected artifact/deliverable references;
- useful lineage-lens references;
- evidence requirements;
- required expertise;
- dependencies and quality gates; and
- the 12 staffed Director assignments recruited to perform that work.

Core exposes the reusable shape. A consuming product owns its private artifact catalogs, identity systems, candidate sources and policies when those details are not suitable for the public engine.

## Stage 6 — Understand execution boundaries

Re-read [Core runtime contract](architecture/core-runtime-contract.md) with the previous concepts in mind.

Key constraints:

- 72 seats do not imply 72 providers or 72 concurrent calls;
- 12 recruited Directors do not imply 12 simultaneous calls;
- explicit binding controls who occupies a seat, not whether a Director is necessarily recruited for the current work;
- candidate cost/default metadata can affect eligibility but never modifies the canonical 100-statement affinity score; and
- historical staffing, policy and plan evidence must remain reconstructible.

## Stage 7 — Before implementation work

Before changing code, a contributor or AI agent should:

1. read every contract referenced by the area being implemented;
2. preserve the provider-neutral public/private boundary;
3. add tests for all stated invariants before claiming implementation complete;
4. keep the sample host intentionally small; and
5. avoid introducing product-specific identity, tenancy or commercial semantics into Core.

## Fast comprehension check

You understand the public Core architecture if you can answer these without private product knowledge:

1. What is the difference between a seat and an occupant candidate?
2. Why can one AI/model occupy all 72 seats?
3. Why might different versions of one provider occupy different seats?
4. What do the 100 scorecard statements measure?
5. What is the difference between candidate eligibility and seat affinity?
6. What does an explicit seat binding override, and what does it not override?
7. Why does Director recruitment select 12 of 25 rather than 12 of 72?
8. What information belongs in an Instrumentation Plan?
9. Why are instrumentation and Director recruitment related?
10. Why is concurrency a separate runtime concern?

If a reader cannot answer these, improve the documentation rather than depending on undocumented project-memory explanations.