# Decision Foundry Core runtime contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core is intended to be a small reusable .NET library that lets any host orchestrate the canonical **72 Decision Foundry agent seats** without requiring the rest of a commercial product.

The public value proposition is deliberately simple: reference the DLL, provide one or more AI/model connectors, and run the 72-seat institution.

Core must remain useful to a single developer or small team without requiring multi-user infrastructure, a recruiter component, tenant management, or private product services.

## 72-seat contract

Core owns a canonical catalog of **72 agent seats**. Each seat has a stable identity and an execution boundary that can obtain an AI/model connector from the host.

The runtime must support at least these binding modes:

1. **One connector for all 72 seats.** A single user can configure one AI/model connection and bind that same connector to every seat. The same model/provider may therefore perform all 72 roles while the roles remain logically distinct.
2. **Multiple shared connectors.** A host may bind groups of seats to different connectors.
3. **Per-seat connectors.** A host may provide a distinct connector for each of the 72 seats.

Core must not assume that 72 seats imply 72 providers, 72 accounts, 72 subscriptions or 72 human users.

## Connector boundary

The public API should make the seat-to-model boundary explicit and replaceable. Exact type names remain an implementation detail until the first code increment, but the contract requires equivalents of:

- a stable `AgentSeatId` for each canonical seat;
- an agent/model connector abstraction;
- a connector resolver or factory that receives the target seat;
- an orchestration runtime that invokes seats through that abstraction rather than directly through a specific AI vendor SDK;
- observable execution state for every seat.

Provider-specific adapters belong behind the connector abstraction. Core may ship reference adapters, but the 72-seat runtime must remain provider-neutral.

## Minimal operating mode

The smallest useful deployment is intentionally small:

```text
1 human user
    |
1 configured AI/model connector
    |
Decision Foundry Core DLL
    |
72 logical agent seats
```

In this mode the same connector services all 72 seats. Core does **not** require a recruiter component to operate.

This operating mode is important for the open-source community because it makes the method usable without recreating enterprise identity, tenancy or user-connection infrastructure.

## Extensibility boundary

Core must allow a host to replace connector resolution and orchestration composition without modifying the 72 canonical seat definitions.

A host may therefore add product-specific participants or use a richer connector-selection policy, but those additions are outside the default Core topology and must not be required for ordinary Core use.

Core does not define private multi-user weighting policies or product-specific identity semantics.

## Execution observability

The runtime must expose enough state for a very small sample host to show what the institution is doing.

At minimum, a sample must be able to distinguish each seat as:

- `Idle`
- `Working`
- `Completed`

Richer outcome/error information may be exposed separately, but the basic monitor must remain understandable without domain-specific UI.

## Reference sample

Core should include at least one intentionally small sample application. Acceptable first forms are:

- Console;
- WinForms; or
- a XAML-based desktop host.

The sample is not a second product. Its purpose is only to demonstrate:

1. configuration of one or more connectors;
2. binding connectors to the 72 seats;
3. starting an orchestration request; and
4. displaying the 72 seat indicators as `Idle`, `Working` or `Completed`.

The first sample should favor the lowest-friction option that proves the DLL contract.

## What Core deliberately does not require

Core must remain independently usable without:

- tenant management;
- organization-specific identity directories;
- one AI account per seat;
- one human user per seat;
- a recruiter participant;
- private per-user weighting logic; or
- a commercial UI.

These are host/product concerns, not prerequisites for the public engine.

## Compatibility rule

Future products may extend the Core composition, but they must consume the public 72-seat contract rather than require Core to understand private product strategy.

The public library remains the reusable engine; product-specific composition remains outside this repository.
