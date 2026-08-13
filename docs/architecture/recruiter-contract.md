# Decision Foundry Core recruiter contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core may expose an **optional reusable Recruiter capability** that helps a host choose the most appropriate AI/model connector candidates for a task.

The Recruiter is not one of the 72 canonical Decision Foundry seats and is not required to run the 72-seat institution. It is an optional composition capability for hosts that have more than one usable connector candidate or want automatic model selection.

Core owns only the generic recruitment mechanism. It must not know whether candidates represent enterprise users, social contacts, service accounts, providers, subscriptions or another product-specific identity model.

## Default behavior: automatic recruitment by affinity

The default Recruiter mode is automatic.

Given a task and a candidate set supplied by the host, the Recruiter evaluates candidate affinity for that task and returns a ranked/weighted selection that the host can use to bind connectors to the 72 seats.

Affinity may consider only information that is legitimately exposed through the public candidate contract, such as:

- model/provider capabilities;
- declared modality or tool support;
- availability/health;
- bounded host-supplied capability metadata;
- prior public or host-supplied performance signals; and
- task requirements.

The exact scoring formula is deliberately not fixed until implementation evidence exists.

## One connection with multiple engines

A single configured AI connection may expose more than one usable engine/model.

For example, a host connector can expose several model endpoints behind one account or platform connection. In that case the Recruiter may choose the engine/model with the strongest affinity for the current task while the same host connection remains the credential/configuration boundary.

Therefore the smallest useful recruited composition may still have only one human/configuration owner while the Recruiter selects among multiple models available through that connection.

## Multiple candidates and periodic reweighting

When the host supplies two or more eligible connector candidates, the Recruiter must support a reusable weighting/ranking model rather than treating every candidate as permanently equivalent.

Weights are expected to be refreshed periodically so that recruitment can adapt to changing capability, availability and observed results. Core must expose this as a configurable scheduling/policy boundary rather than hard-code a product cadence.

A consuming product may choose a weekly, monthly or other bounded reevaluation interval. The exact default cadence is intentionally deferred until product evidence supports one.

## Candidate-set boundary

Core must separate **where candidates come from** from **how candidates are ranked**.

The host supplies an already-authorized candidate set. Core may rank/reweight that set, but it must not decide who is a valid enterprise user, who is a social contact or who belongs to a manually selected team.

Conceptually:

```text
Host-specific candidate source
        |
        v
Authorized candidate set
        |
        v
Core Recruiter
(task affinity + ranking/weighting)
        |
        v
Connector selection/resolution
        |
        v
72 canonical seats
```

A host may deliberately pre-scope the candidate set. Core does not need to know whether that scope was produced automatically or by an explicit product-level choice.

## Public abstraction requirements

Exact type names remain an implementation decision, but the public contract requires equivalents of:

- a task/request descriptor suitable for recruitment;
- an opaque candidate identity;
- one or more connector/model options associated with a candidate;
- capability/availability metadata needed for affinity evaluation;
- a ranked or weighted recruitment result;
- a policy boundary for periodic reevaluation; and
- an observable Recruiter execution state when a host chooses to surface it.

The public API must remain provider-neutral.

## Relationship to the 72-seat runtime

The canonical Core topology remains exactly 72 seats.

A host that does not enable recruitment can bind one connector to all 72 seats and never instantiate the Recruiter.

A host that enables recruitment may use the Recruiter to resolve which connector candidate or model should serve one or more seats. That does not add a 73rd canonical seat or change any `AgentSeatId`.

## Privacy and product boundary

Core must not require or persist product-specific concepts such as:

- tenant identifiers;
- enterprise user directories;
- social/contact graphs;
- private per-person weighting semantics;
- product-specific manual team assignment rules; or
- private credential ownership relationships.

Those belong to consuming products. Core receives only the minimum generic candidate/connector information needed to perform recruitment.

## Validation required before implementation is called complete

At minimum, tests must demonstrate that:

1. recruitment is optional and the 72-seat runtime still works without it;
2. one connection exposing multiple models can be ranked by task affinity;
3. two or more candidates can produce deterministic, inspectable ranked/weighted results for a fixed input;
4. periodic reevaluation is controlled through an injected/configurable policy rather than a hard-coded product cadence;
5. a host-supplied pre-scoped candidate set is respected; and
6. no product-specific identity or relationship model is required by the public Recruiter contract.
