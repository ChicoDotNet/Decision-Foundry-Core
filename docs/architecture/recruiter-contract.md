# Decision Foundry Core recruiter contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core exposes a reusable Recruiter capability whose institutional selection responsibility is **to choose 12 of the 25 canonical Directors for a work request**.

The Recruiter is not one of the 72 canonical seats. It does not choose whether Elders, the Chief Delivery Officer, Vice Presidents or the Strategist exist in the flow; those roles participate according to their structural/governance contracts.

Core owns only the generic recruitment mechanism. It must not know whether occupants represent Enterprise users, social contacts, service accounts, providers or another product-specific identity model.

## Recruitment unit: Director seat + occupant

The atomic candidate evaluated by the Recruiter is:

```text
Director role/task profile
        +
occupant connector/model profile
        =
Director assignment candidate
```

The Recruiter evaluates **D01-D25 only** for cohort inclusion.

A single occupant may be eligible for all 25 Directors. In the smallest community deployment one AI/model connection may occupy every Director candidate while the Director roles remain logically distinct.

## Default behavior: automatic recruitment by affinity

Given a work request and a host-supplied authorized occupant/candidate set, the Recruiter:

1. evaluates each of the 25 Director role profiles against the work;
2. evaluates the eligible occupant/model options for each Director role;
3. forms inspectable Director-seat + occupant assignment affinities; and
4. selects **exactly 12 distinct Director seats with their chosen occupants**.

The resulting top-12 cohort is the domain layer recruited for that work request.

Affinity may consider legitimately exposed information such as:

- the Director's canonical mission, responsibilities, expected outputs and quality focus;
- model/provider capabilities;
- modality/tool support;
- availability/health;
- bounded host-supplied capability metadata;
- prior public or host-supplied performance signals; and
- the current work requirements.

The exact scoring formula remains an implementation decision until evidence exists.

## One connection with multiple engines

A single configured AI connection may expose more than one usable engine/model.

The Recruiter may choose different engines/models from that same connection for different Director assignments when their role/work affinity differs.

Therefore one user does not imply one fixed model for all 12 recruited Directors.

## Multiple occupants and periodic reweighting

When the host supplies two or more eligible occupant candidates, the Recruiter supports reusable weighting/ranking rather than treating every occupant as permanently equivalent.

Core exposes periodic reevaluation as a configurable policy boundary rather than hard-coding a commercial cadence. Product-specific weekly/monthly policy belongs to consuming products.

## Roles not subject to top-12 recruitment

The top-12 competition applies only to D01-D25.

Other canonical roles are activated by the orchestration/governance contract rather than ranked against the Directors:

- Elders enter when their Council stage applies;
- the Chief Delivery Officer / flow role observes relevant transitions by default;
- VP and Strategist synthesis follows the institutional flow;
- transversal and dynamic roles execute when their task contracts require them.

These roles may still require connector/occupant resolution, but they are not candidates for one of the 12 Director slots.

## Candidate-set boundary

Core separates **where occupants come from** from **which Director assignments are strongest**.

The host supplies an already-authorized occupant candidate set. Core may rank/reweight that set and combine it with D01-D25, but it does not decide who is a valid Enterprise user, social contact or manually selected team member.

Conceptually:

```text
Authorized occupant candidates
            |
            +--------------------------+
                                       |
25 canonical Director profiles        |
            |                          |
            +------------+-------------+
                         |
                         v
                   Core Recruiter
                seat fit + occupant fit
                         |
                         v
              25 Director assignments
                         |
                         v
              top 12 Director cohort
                         |
                         v
               institutional flow
```

## Public abstraction requirements

Exact type names remain an implementation decision, but the public contract requires equivalents of:

- work/request descriptor;
- stable canonical Director identity and profile;
- opaque occupant/candidate identity;
- connector/model options associated with an occupant;
- capability/availability metadata;
- Director assignment candidate;
- inspectable affinity/ranking result;
- **12-Director recruitment result**;
- configurable reevaluation policy; and
- observable Recruiter execution state.

The public API remains provider-neutral.

## Relationship to the 72-seat institution

The institution contains 72 canonical seats. The Recruiter does **not** pick 12 of 72.

It picks 12 of the **25 Directors**. Governance, synthesis, transversal and flow roles remain available and participate according to their own contracts.

The 12 Directors also do not imply 12 simultaneous calls: runtime scheduling remains bounded, dependency-aware and stage-aware.

## Validation required before implementation is complete

Tests must demonstrate that:

1. exactly 25 Director candidates exist for recruitment;
2. a valid recruited work request selects exactly 12 distinct Director seats;
3. no Elder, VP, Strategist, transversal or flow seat can consume one of those 12 Director slots;
4. one occupant may validly occupy all 25 Director candidates and multiple selected Directors;
5. one connection exposing multiple models can produce different model choices by Director/work affinity;
6. two or more occupants can produce deterministic, inspectable ranked/weighted results for a fixed input;
7. host-supplied candidate scoping is respected; and
8. no product-specific identity or relationship model is required by the public Recruiter contract.
