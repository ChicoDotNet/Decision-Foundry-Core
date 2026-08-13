# Decision Foundry Core recruiter contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core exposes a reusable Recruiter capability with **two distinct responsibilities**:

1. **institutional staffing** — choose the best available occupant for each of the 72 canonical seats using the deterministic [100-statement seat scorecard](seat-scorecard-contract.md); and
2. **work-specific Director recruitment** — from the 25 already occupied Director seats, select the 12 Directors best suited to a concrete work request.

The Recruiter is not one of the 72 canonical seats. Core owns only the generic recruitment/scoring mechanism and must not know whether occupants represent Enterprise users, social contacts, service accounts, providers or another product-specific identity model.

The guiding principle is **best person for the job**.

## Stage 1 — institutional staffing of all 72 seats

Every canonical seat owns a versioned job scorecard of exactly **100 positive statements** describing the ideal occupant for that seat.

For each eligible `OccupantCandidate`, the connected AI answers every statement from the perspective of the user/owner bound to that candidate with a percentage in `0..100`.

The Core v1 staffing score is deliberately simple:

```text
SeatAffinity = arithmetic mean of the 100 percentages
```

Every statement has equal weight. There is no hidden provider, model-prestige, popularity or product-specific multiplier in the public v1 score.

The Recruiter evaluates the eligible candidates against each of the 72 scorecards and selects the candidate with the highest affinity for that seat.

```text
100-statement canonical seat scorecard
        +
100 candidate agreement percentages
        =
SeatAffinity 0..100
```

The Recruiter therefore produces an occupied 72-seat institution by applying the same deterministic rule to every seat.

A single occupant may fill many or all seats when it is the best available candidate. Core must not impose artificial uniqueness or diversity constraints that override `best person for the job`.

For every staffed seat the result must preserve enough evidence to reconstruct:

- scorecard identifier/version;
- the complete 100-answer vector for the selected occupant;
- final arithmetic-mean affinity;
- which occupant/model was selected;
- the candidate-universe snapshot used; and
- alternative affinity results when retained by host policy.

Operational availability determines whether a candidate is eligible to execute; it does not alter the stored scorecard affinity.

### Candidate perspective

An occupant candidate is bound to one AI connection/model and one user/owner perspective for that candidate.

Core does not need the user's identity. It only requires a stable opaque candidate identity and the candidate's scorecard responses.

The canonical assessment question is conceptually:

> From the perspective of your user, what percentage do you agree with this statement?

When one human exposes several independently connected AI/model candidates, each candidate is evaluated independently.

### One candidate / one connection

In the smallest community deployment, one user may provide one AI/model candidate. With no competing candidate, that occupant can fill all 72 seats.

If a host exposes several model candidates through one connection, each concrete candidate may be scored independently and the best candidate may differ by seat.

The scorecard may be assessed through one structured model call or bounded batches; the contract does **not** require 100 network calls per seat.

## Stage 2 — recruit 12 of the 25 occupied Directors for the work

Once all 72 seats have occupants, the Recruiter evaluates only the **25 occupied Director seats D01-D25** for a concrete work request.

The staffing score already answers whether the current occupant is a strong fit for the Director seat. Work-specific Director recruitment answers the separate question of whether that occupied Director domain is relevant to the current assignment.

The Recruiter selects **exactly 12 distinct Directors** as the domain cohort for that work.

The result preserves the Director seat + current occupant combination. The same occupant may appear in multiple selected Director seats when that occupant won those seats during institutional staffing.

The exact work-relevance formula for selecting 12-of-25 is a separate implementation contract and is not invented by the seat scorecard algorithm.

## Roles outside the top-12 Director competition

The top-12 competition applies only to D01-D25.

Other canonical roles already have occupants from Stage 1 and activate according to their institutional contracts:

- Elders enter when their Council stage applies;
- the Chief Delivery Officer / flow role observes relevant transitions by default;
- Vice Presidents and the Strategist participate in the synthesis chain;
- transversal roles activate when their capability contracts are needed.

They never consume one of the 12 Director slots.

## Reevaluation

When a host supplies more than one eligible occupant candidate, the consuming product may periodically reassess candidates against the versioned seat scorecards and restaff the 72-seat institution.

Core exposes the deterministic assessment, ranking, snapshot and versioning mechanics. It does not hard-code a commercial reevaluation cadence or product identity rules.

A completed staffing/recruitment decision must remain reconstructible after later reassessment. New scorecard or candidate assessments create new immutable snapshots rather than rewriting historical results.

## Candidate-set boundary

Core separates **where occupants come from** from **how they are matched to seats**.

The host supplies an already-authorized occupant candidate set. Core scores that set against all 72 seat scorecards and later ranks the 25 staffed Directors for a work request, but Core does not decide who is a valid Enterprise user, social contact or manually selected team member.

Conceptually:

```text
Authorized occupant candidates
             |
             v
100-statement assessment per seat/candidate
             |
             v
      Institutional staffing
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
- versioned 100-statement scorecard per seat;
- opaque occupant/candidate identity bound to one perspective;
- connector/model identifier associated with an occupant candidate;
- 100-value assessment vector in `0..100`;
- deterministic arithmetic-mean seat affinity;
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

1. all 72 canonical seats have a scorecard of exactly 100 statements;
2. all eligible candidate responses are constrained to `0..100`;
3. v1 `SeatAffinity` is exactly the arithmetic mean of the 100 responses;
4. the highest-affinity eligible candidate is selected for each seat;
5. all 72 seats receive an occupant in a complete staffing snapshot;
6. the same occupant can validly fill multiple or all seats;
7. staffing results preserve the complete scorecard vector and versions needed for reconstruction;
8. exactly 25 occupied Director candidates exist for work-specific recruitment;
9. a valid work request selects exactly 12 distinct Director seats;
10. no Elder, VP, Strategist, transversal or flow seat can consume one of those 12 Director slots;
11. host-supplied candidate scoping is respected; and
12. later reassessment does not rewrite historical staffing/recruitment evidence.
