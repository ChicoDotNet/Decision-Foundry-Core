# Decision Foundry Core recruiter contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core exposes a reusable Recruiter capability with **two distinct responsibilities**:

1. **institutional staffing** — choose the best available occupant for each of the 72 canonical seats; and
2. **work-specific Director recruitment** — from the 25 already occupied Director seats, select the 12 Directors best suited to a concrete work request.

The Recruiter is not one of the 72 canonical seats. Core owns only the generic recruitment mechanism and must not know whether occupants represent Enterprise users, social contacts, service accounts, providers or another product-specific identity model.

The guiding principle is **best person for the job**.

## Stage 1 — institutional staffing of all 72 seats

For each canonical seat, the Recruiter evaluates the available occupant/model candidates against that seat's executable role profile.

The staffing unit is:

```text
canonical seat profile
        +
occupant connector/model profile
        =
SeatOccupancyAffinity
```

The Recruiter produces an occupied 72-seat institution by selecting the strongest available occupant for each seat.

A single occupant may fill many or all seats when it is the best available candidate. Core must not impose artificial uniqueness between occupants and seats.

For every staffed seat the result must be inspectable enough to reconstruct:

- which occupant/model was selected;
- which alternatives were considered;
- the affinity/ranking evidence available to Core; and
- the seat-profile version used for the decision.

### One connection with multiple engines

A single configured AI connection may expose more than one usable engine/model. The Recruiter may therefore select different models from the same connection for different seats according to their role fit.

In the smallest community deployment one user may provide one connection that services all 72 seats. If that connection exposes only one model, that model can occupy every seat. If it exposes several models, the Recruiter may choose among them per seat.

## Stage 2 — recruit 12 of the 25 occupied Directors for the work

Once all 72 seats have occupants, the Recruiter evaluates only the **25 occupied Director seats D01-D25** for a concrete work request.

The work-selection unit is:

```text
Director seat profile
        +
current occupant profile
        +
work request
        =
DirectorWorkAffinity
```

The Recruiter selects **exactly 12 distinct Directors** as the domain cohort for that work.

The result must preserve the seat+occupant combination. The same occupant may appear in multiple selected Director seats when that is the strongest available staffing.

## Roles outside the top-12 Director competition

The top-12 competition applies only to D01-D25.

Other canonical roles already have occupants from Stage 1 and activate according to their institutional contracts:

- Elders enter when their Council stage applies;
- the Chief Delivery Officer / flow role observes relevant transitions by default;
- Vice Presidents and the Strategist participate in the synthesis chain;
- transversal roles activate when their capability contracts are needed.

They never consume one of the 12 Director slots.

## Multiple occupants and periodic reweighting

When a host supplies two or more eligible occupant candidates, staffing must not treat them as permanently equivalent.

Core supports a reusable weighting/ranking input and configurable reevaluation policy. A consuming product can refresh occupant weights periodically and then restaff seats using the latest approved snapshot.

Core does not hard-code a product cadence, identity model or social relationship rule.

A completed staffing/recruitment decision must remain reconstructible even after later weight recalculation.

## Candidate-set boundary

Core separates **where occupants come from** from **how they are matched to seats**.

The host supplies an already-authorized occupant candidate set. Core may rank/reweight that set against all 72 seat profiles and later rank the 25 staffed Directors for a work request, but Core does not decide who is a valid Enterprise user, social contact or manually selected team member.

Conceptually:

```text
Authorized occupant candidates
             |
             v
      Institutional staffing
      72 seat-profile matches
             |
             v
       72 occupied seats
             |
             +-------------------------------+
                                             |
                                  D01-D25 occupied Directors
                                             |
                                             v
                                    Work-specific ranking
                                             |
                                             v
                                    12-Director cohort
                                             |
                                             v
                                     institutional flow
```

## Public abstraction requirements

Exact type names remain an implementation decision, but the public contract requires equivalents of:

- stable canonical seat identity/profile for all 72 seats;
- opaque occupant/candidate identity;
- one or more connector/model options associated with an occupant;
- capability/availability metadata;
- seat-occupant affinity result;
- an occupied-seat assignment;
- an immutable/versioned 72-seat staffing snapshot;
- work/request descriptor;
- Director-work affinity result;
- a **12-Director recruitment result**;
- configurable reevaluation policy; and
- observable Recruiter execution state.

The public API remains provider-neutral.

## Concurrency is separate from recruitment

Neither 72 staffed seats nor 12 recruited Directors imply that all corresponding external calls run simultaneously.

Runtime scheduling remains bounded, dependency-aware and stage-aware. Independent work may fan out concurrently, while dependent layers execute in separate waves.

## Validation required before implementation is complete

Tests must demonstrate that:

1. all 72 canonical seats receive an occupant in a complete staffing snapshot;
2. the same occupant can validly fill multiple or all seats;
3. different models from one connection can be selected for different seats;
4. staffing results are deterministic/inspectable for fixed inputs and profile/weight versions;
5. exactly 25 occupied Director candidates exist for work-specific recruitment;
6. a valid work request selects exactly 12 distinct Director seats;
7. no Elder, VP, Strategist, transversal or flow seat can consume one of those 12 Director slots;
8. two or more occupants can be ranked/weighted without leaking product-specific identity semantics into Core;
9. host-supplied candidate scoping is respected; and
10. later reweighting does not rewrite historical staffing/recruitment evidence.
