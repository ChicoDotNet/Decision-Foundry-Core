# Decision Foundry Core runtime contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core is intended to be a small reusable .NET library that exposes the canonical **72 Decision Foundry seats** as an available institutional pool and executes work through a **12-agent cohort selected for the specific assignment**.

The public value proposition is deliberately simple: reference the DLL, provide one or more AI/model connectors, form seat/occupant assignments, select the 12 combinations with the strongest affinity for the work, and execute that cohort.

Core must remain useful to a single developer or small team without requiring multi-user infrastructure, an enabled Recruiter, tenant management or private product services.

## 72-seat institutional pool

Core owns a canonical catalog of **72 agent seats**. Each seat has a stable identity, role/task profile and an execution boundary that can obtain an AI/model connector from the host.

The 72 seats are **available roles**, not 72 simultaneously executing workers.

For a particular work request, a logical agent is the combination of:

```text
canonical seat
    +
occupant / connector-model candidate
    =
agent assignment
```

The same occupant may be used for many seats. A single-user community host may therefore use one AI connection as the occupant source for all 72 seat assignments while preserving the distinct role of every seat.

Core must not assume that 72 seats imply 72 providers, 72 accounts, 72 subscriptions or 72 human users.

## 12-agent execution cohort

A normal Decision Foundry work request is executed by **exactly 12 selected agent assignments** from the 72-seat pool.

Selection is based on the combination of:

- the seat's role/capabilities and fit for the work; and
- the occupant's connector/model capabilities and affinity for that same work.

Therefore selection is not merely "choose 12 seats" and it is not merely "choose the best model". The selection unit is the **seat + occupant pair**.

Conceptually:

```text
72 canonical seats
        x
eligible occupants/connectors/models
        |
        v
72 available seat/occupant assignments
        |
        v
rank by assignment affinity
        |
        v
12 selected agent assignments
        |
        v
orchestrated execution
```

The 12 selected agents do not have to execute concurrently. Core may schedule them sequentially, in bounded parallel groups or through fan-out/fan-in stages according to the orchestration contract. The invariant is the selected cohort size, not a concurrency count.

If the optional Recruiter is disabled, the host must supply the 12 selected seat/occupant assignments explicitly. If the Recruiter is enabled, it produces the 12-agent cohort.

## Connector boundary

The public API should make the seat-to-occupant/model boundary explicit and replaceable. Exact type names remain an implementation detail until the first code increment, but the contract requires equivalents of:

- a stable `AgentSeatId` for each canonical seat;
- a stable seat/task profile;
- an opaque occupant/candidate identity;
- an agent/model connector abstraction;
- a connector resolver or factory;
- a seat/occupant assignment value;
- a 12-assignment execution cohort;
- an orchestration runtime that invokes selected assignments through abstractions rather than directly through a specific AI vendor SDK; and
- observable execution state.

Provider-specific adapters belong behind the connector abstraction. Core may ship reference adapters, but the runtime must remain provider-neutral.

## Minimal operating mode

The smallest useful deployment is intentionally small:

```text
1 human user
    |
1 configured AI/model connection
    |
1 occupant source reused across all seats
    |
72 possible seat/occupant assignments
    |
12 selected assignments for this work
    |
Decision Foundry Core execution
```

In this mode one AI/model connection may occupy every candidate seat, but only the 12 seat/occupant combinations selected for the current assignment execute.

Core does **not** require a Recruiter to operate if the host supplies the 12 assignments itself.

This operating mode makes the method usable without recreating enterprise identity, tenancy or user-connection infrastructure.

## Optional recruitment capability

Core may also expose the reusable [Recruiter contract](recruiter-contract.md).

The Recruiter is **not** a canonical seat. It is an optional composition capability that:

1. evaluates eligible occupants/connectors/models;
2. determines the strongest seat/occupant assignment for each canonical role where relevant; and
3. selects the **12 seat/occupant combinations** with the strongest overall affinity for the requested work.

A host that does not need recruitment can ignore this capability and provide the 12 assignments directly.

## Extensibility boundary

Core must allow a host to replace connector resolution, candidate sourcing and orchestration composition without modifying the 72 canonical seat definitions or the 12-agent cohort invariant.

A host may provide an already-scoped candidate set or use richer connector-selection policy. Product-specific identity, relationship and weighting semantics remain outside the public engine.

## Execution observability

The runtime must expose enough state for a very small sample host to show the institutional pool and the selected cohort.

At minimum every seat must expose:

- whether it is selected for the current work; and
- execution state such as `Idle`, `Working` or `Completed`.

A typical run therefore shows 72 seats, 12 marked as selected and 60 remaining available/idle for that work request.

Richer outcome/error information may be exposed separately. If a host enables the optional Recruiter, its execution state should be observable separately from the 72 canonical seats.

## Reference sample

Core should include at least one intentionally small sample application. Acceptable first forms are:

- Console;
- WinForms; or
- a XAML-based desktop host.

The sample is not a second product. Its purpose is only to demonstrate:

1. configuration of one or more connectors;
2. formation of seat/occupant candidates;
3. selection of the 12-agent cohort;
4. execution of that cohort; and
5. display of all 72 seats with selected/non-selected status plus `Idle`, `Working` or `Completed` execution state.

The first sample should favor the lowest-friction option that proves the DLL contract.

## What Core deliberately does not require

Core must remain independently usable without:

- tenant management;
- organization-specific identity directories;
- one AI account per seat;
- one human user per seat;
- an enabled Recruiter when the host supplies the 12 assignments;
- private per-user weighting logic;
- social/contact graphs; or
- a commercial UI.

These are host/product concerns, not prerequisites for the public engine.

## Compatibility rule

Future products may extend the Core composition, but they must consume the public 72-seat pool, 12-agent cohort and optional recruitment contracts rather than require Core to understand private product strategy.

The public library remains the reusable engine; product-specific candidate sources, identity semantics and composition remain outside this repository.
