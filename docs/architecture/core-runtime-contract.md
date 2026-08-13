# Decision Foundry Core runtime contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core is intended to be a small reusable .NET library that exposes the canonical **72-seat Decision Foundry institution** through provider-neutral connectors and orchestration contracts.

The 72 seats are the permanent institutional roles, not a claim that 72 model calls execute concurrently.

For each concrete work request, the Recruiter selects **12 of the 25 Director seats** as the domain cohort best suited to the assignment. The remaining institutional roles participate according to their existing structural/governance contracts rather than competing for one of those 12 Director positions.

## 72-seat institution

Core owns a canonical catalog of 72 stable seats:

- 1 Strategist;
- 5 Vice Presidents;
- 25 Directors;
- 12 transversal production/platform roles;
- 28 Elders in four governance councils; and
- 1 Chief Delivery Officer / flow role.

Every seat has a stable identity and an executable role profile. A host must be able to resolve an AI/model connector occupant for any seat that becomes active.

A single-user community host may bind the same AI/model connection to every one of the 72 seats. The seats remain logically distinct even when their occupant source is the same.

Core must not assume that 72 seats imply 72 providers, accounts, subscriptions, human users or concurrent calls.

## Recruitable layer: 12 of 25 Directors

Director recruitment operates only over D01-D25.

For the current work request, the Recruiter evaluates each Director assignment as:

```text
Director seat profile
        +
occupant / connector-model profile
        =
Director assignment affinity
```

It then selects **exactly 12 distinct Director seats with their chosen occupants**.

The selection unit is therefore the combination of the Director role and its occupant. A very capable model occupying a Director seat irrelevant to the assignment does not automatically outrank the correct Director role, and a highly relevant Director seat paired with an incapable occupant is likewise not a strong assignment.

The same occupant may occupy many Director candidates and may occupy multiple selected Directors. In the simplest one-user/one-AI deployment, the same occupant may fill all 25 Director candidates while the Recruiter still chooses the 12 Director roles with the best fit for the assignment.

## Institutional roles outside Director recruitment

Roles outside D01-D25 are **not** candidates in the top-12 Director competition.

Their participation follows the Decision Foundry organizational flow:

- Councils of Elders activate by default when a material transition reaches their assigned governance stage;
- the Chief Delivery Officer / flow role observes and escalates according to its procedural contract;
- Strategist and Vice Presidents participate according to the synthesis chain;
- transversal roles and dynamic execution roles participate according to the task/orchestration contracts that require them.

Therefore a run may involve more than 12 active institutional agents over its lifecycle even though the recruited **Director cohort is exactly 12**.

## Concurrency is a separate concern

The 12 recruited Directors do not have to execute simultaneously.

Core orchestration may execute independent work concurrently, but concurrency remains bounded and stage-aware. Sequential work, bounded parallel waves and fan-out/fan-in are all valid according to dependency structure, budgets and rate limits.

The invariant is **12 recruited Directors**, not 12 concurrent requests and certainly not 72 concurrent requests.

## Connector boundary

The public API should make role and occupant/model resolution explicit and replaceable. Exact type names remain an implementation detail until the first code increment, but the contract requires equivalents of:

- stable seat identity for all 72 seats;
- executable seat profiles;
- an opaque occupant/candidate identity;
- provider-neutral model connector abstraction;
- connector/occupant resolver;
- Director seat/occupant assignment value;
- a 12-Director recruitment result;
- orchestration that invokes active roles through abstractions rather than directly through a vendor SDK; and
- observable execution state.

Provider-specific adapters belong behind the connector abstraction.

## Minimal operating mode

The smallest useful deployment is intentionally small:

```text
1 human user
    |
1 configured AI/model connection
    |
same occupant available to all 72 seats
    |
Recruiter evaluates D01-D25
    |
12 Directors selected for the work
    |
required synthesis / governance / flow roles activate by contract
```

The Recruiter itself does not need a separate model connection merely to make the trivial one-occupant case possible.

## Optional recruitment capability

Core exposes the reusable [Recruiter contract](recruiter-contract.md) for Director selection and occupant affinity.

A host may provide an already-selected 12-Director cohort instead, but a conforming recruited run selects 12 of the canonical 25 Directors.

## Execution observability

The runtime must expose enough state for a small sample host to show:

- all 72 canonical seats;
- which 12 Directors were recruited for the current work;
- the occupant/model bound to active seats when disclosure is appropriate; and
- execution state such as `Idle`, `Working` or `Completed`.

Governance/flow roles must be distinguishable from the recruited Director cohort rather than displayed as rejected Director candidates.

## Reference sample

Core should include at least one intentionally small Console, WinForms or XAML sample whose purpose is to prove the DLL contract.

The sample should demonstrate:

1. connector configuration;
2. the 72-seat catalog;
3. recruitment of 12 of 25 Directors;
4. role+occupant selection visibility;
5. activation of required structural/governance roles as the flow advances; and
6. execution-state indicators.

## Compatibility rule

Future products may extend candidate sourcing, weighting and composition, but they must preserve the public 72-seat institution and the 12-of-25 Director recruitment contract unless a later explicit versioned decision supersedes it.

Product-specific identity, tenant, social-graph and private weighting semantics remain outside Core.
