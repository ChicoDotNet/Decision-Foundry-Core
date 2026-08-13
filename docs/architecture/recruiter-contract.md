# Decision Foundry Core recruiter contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core may expose an **optional reusable Recruiter capability** that forms the best 12-agent execution cohort for a work request from the canonical 72-seat institutional pool.

The Recruiter is not one of the 72 canonical Decision Foundry seats. It is a composition capability that evaluates both **seat fit** and **occupant fit**.

Core owns only the generic recruitment mechanism. It must not know whether occupants represent enterprise users, social contacts, service accounts, providers, subscriptions or another product-specific identity model.

## Recruitment unit: seat + occupant

The atomic candidate evaluated by the Recruiter is not a seat alone and not a model/connector alone.

It is the combination:

```text
seat role/task profile
        +
occupant connector/model profile
        =
agent assignment candidate
```

A single occupant may be eligible for many or all 72 seats. For example, in the smallest community deployment one AI/model connection may occupy all 72 seat candidates while the seat roles remain logically distinct.

## Default behavior: automatic recruitment by affinity

Given a work request and a host-supplied authorized occupant/candidate set, the Recruiter:

1. evaluates occupant/model affinity against the role and task profile of the 72 canonical seats;
2. forms the available seat/occupant assignment candidates;
3. ranks those assignments for the specific work request; and
4. selects **exactly 12 distinct canonical seats with their chosen occupants** as the execution cohort.

The selected cohort is therefore a set of 12 seat/occupant pairs.

Affinity may consider only information legitimately exposed through public contracts, such as:

- the canonical seat role/task profile;
- model/provider capabilities;
- declared modality or tool support;
- availability/health;
- bounded host-supplied capability metadata;
- prior public or host-supplied performance signals; and
- task requirements.

The exact scoring formula is deliberately not fixed until implementation evidence exists.

## One connection with multiple engines

A single configured AI connection may expose more than one usable engine/model.

The Recruiter may choose different engines/models from the same connection for different seat assignments when their affinity differs by role or work requirement.

Therefore one human/configuration owner does not imply one fixed model, and one model/connection may still occupy multiple selected seats.

## Multiple candidates and periodic reweighting

When the host supplies two or more eligible occupant candidates, the Recruiter must support reusable weighting/ranking rather than treating every occupant as permanently equivalent.

Weights are expected to be refreshed periodically so recruitment can adapt to changing capability, availability and observed results. Core exposes this as a configurable scheduling/policy boundary rather than hard-coding a product cadence.

A consuming product may choose a weekly, monthly or other bounded reevaluation interval. The exact default cadence is intentionally deferred until product evidence supports one.

## Candidate-set boundary

Core separates **where occupants/candidates come from** from **how seat/occupant assignments are ranked**.

The host supplies an already-authorized occupant candidate set. Core may rank/reweight that set and combine it with the 72 canonical seats, but it must not decide who is a valid enterprise user, who is a social contact or who belongs to a manually selected team.

Conceptually:

```text
Host-specific occupant source
        |
        v
Authorized occupant candidates
        |
        +--------------------+
                             |
72 canonical seat profiles  |
        |                    |
        +---------+----------+
                  |
                  v
            Core Recruiter
     seat fit + occupant fit
                  |
                  v
72 available seat/occupant assignments
                  |
                  v
       top 12 assignment cohort
                  |
                  v
        orchestrated execution
```

A host may deliberately pre-scope the occupant set. Core does not need to know whether that scope was produced automatically or by an explicit product-level choice.

## Public abstraction requirements

Exact type names remain an implementation decision, but the public contract requires equivalents of:

- a task/request descriptor suitable for recruitment;
- stable canonical seat identity and task profile;
- an opaque occupant/candidate identity;
- one or more connector/model options associated with an occupant;
- capability/availability metadata needed for affinity evaluation;
- a seat/occupant assignment candidate;
- an inspectable affinity/ranking result;
- a **12-assignment recruitment result**;
- a policy boundary for periodic reevaluation; and
- an observable Recruiter execution state.

The public API must remain provider-neutral.

## Relationship to the 72-seat runtime

The canonical Core institution remains exactly 72 seats, but a work request does **not** execute all 72.

The Recruiter selects 12 seat/occupant assignments for the requested work. The remaining 60 seats remain available/idle for that request.

If recruitment is disabled, the host must provide a valid 12-assignment cohort directly.

The 12 selected agents do not imply 12 simultaneous external calls. Runtime scheduling may be sequential, bounded-parallel or staged fan-out/fan-in.

## Privacy and product boundary

Core must not require or persist product-specific concepts such as:

- tenant identifiers;
- enterprise user directories;
- social/contact graphs;
- private per-person weighting semantics;
- product-specific manual team assignment rules; or
- private credential ownership relationships.

Those belong to consuming products. Core receives only the minimum generic occupant/connector information needed to perform recruitment.

## Validation required before implementation is called complete

At minimum, tests must demonstrate that:

1. the canonical seat pool contains exactly 72 stable seats;
2. a valid work request produces exactly 12 selected seat/occupant assignments;
3. selected assignments use 12 distinct canonical seats;
4. one occupant/connection may validly occupy all 72 candidates and multiple selected seats;
5. one connection exposing multiple models can produce different model choices by seat/task affinity;
6. two or more occupants can produce deterministic, inspectable ranked/weighted results for a fixed input;
7. periodic reevaluation is controlled through an injected/configurable policy rather than a hard-coded product cadence;
8. a host-supplied pre-scoped candidate set is respected; and
9. no product-specific identity or relationship model is required by the public Recruiter contract.
