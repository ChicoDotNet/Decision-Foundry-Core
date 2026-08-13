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

## Institutional staffing modes

Core supports automatic, explicit and mixed staffing.

### Automatic staffing

When a seat has no explicit binding, the Recruiter uses the canonical 100-statement scorecard for that seat and selects the eligible occupant candidate with the highest arithmetic-mean affinity.

### Explicit staffing

A host/user may explicitly choose which eligible AI/connector candidate occupies a seat. This public capability is defined by the [Explicit seat binding contract](seat-binding-contract.md).

An explicit binding takes precedence over automatic Recruiter selection for that seat. The Recruiter does not silently replace the selected candidate merely because another candidate has a higher score.

### Mixed staffing

A host may pin some seats explicitly and allow the Recruiter to fill every remaining seat automatically.

Conceptually:

```text
72 canonical seats
        |
        +-- explicit binding? --> chosen connector/occupant
        |
        +-- otherwise --------> Recruiter scorecard winner
        |
        v
72 occupied seats
```

The staffing snapshot must preserve whether every seat was selected automatically or explicitly.

## Recruitable layer: 12 of 25 Directors

Director recruitment operates only over D01-D25 after the institution has been staffed.

The Recruiter then selects **exactly 12 distinct occupied Director seats** for the current work request according to work-specific relevance.

The same occupant may occupy many Director seats and may appear in multiple selected Directors.

An explicit seat binding controls **who occupies a Director seat**; it does not automatically force that Director into the top-12 work cohort.

For example:

```text
Explicit binding: D17 -> Connector B
          |
          v
D17 is occupied by Connector B
          |
          v
D17 still competes with D01-D25 for one of 12 work slots
```

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
- executable seat profiles and versioned scorecards;
- an opaque occupant/candidate identity;
- provider-neutral model connector abstraction;
- connector/occupant resolver;
- explicit seat-binding value/provenance;
- automatic staffing result/provenance;
- immutable/versioned 72-seat staffing snapshot;
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
automatic or explicit seat bindings
    |
72 occupied seats
    |
Recruiter evaluates D01-D25
    |
12 Directors selected for the work
    |
required synthesis / governance / flow roles activate by contract
```

The Recruiter itself does not need a separate model connection merely to make the trivial one-occupant case possible.

## Recruitment and binding capabilities

Core exposes:

- the reusable [Recruiter contract](recruiter-contract.md);
- the deterministic [100-statement seat scorecard](seat-scorecard-contract.md); and
- the [Explicit seat binding contract](seat-binding-contract.md).

A host may therefore choose between automatic staffing, fully manual connector selection or a hybrid of both without changing the 72-seat institution.

## Execution observability

The runtime must expose enough state for a small sample host to show:

- all 72 canonical seats;
- occupant/model bound to each seat when disclosure is appropriate;
- whether that occupant was chosen by the Recruiter or by explicit binding;
- which 12 Directors were recruited for the current work; and
- execution state such as `Idle`, `Working` or `Completed`.

Governance/flow roles must be distinguishable from the recruited Director cohort rather than displayed as rejected Director candidates.

## Reference sample

Core should include at least one intentionally small Console, WinForms or XAML sample whose purpose is to prove the DLL contract.

The sample should demonstrate:

1. configuration of one or more connectors;
2. the 72-seat catalog;
3. optional manual selection of which connector occupies selected seats;
4. automatic staffing of remaining seats;
5. recruitment of 12 of 25 Directors;
6. role+occupant selection provenance;
7. activation of required structural/governance roles as the flow advances; and
8. execution-state indicators.

## Compatibility rule

Future products may extend candidate sourcing, identity presentation and composition, but they must preserve the public 72-seat institution, explicit-binding semantics and 12-of-25 Director recruitment contract unless a later explicit versioned decision supersedes them.

Product-specific identity, tenant and social-graph semantics remain outside Core.
