# Canonical 100-statement seat scorecard contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core uses a deterministic, inspectable scorecard to measure how well an available AI occupant fits each of the 72 canonical seats.

The mechanism intentionally follows the recruiting philosophy **best person for the job**. It does not rely on a hidden composite heuristic for institutional staffing.

Each canonical seat owns one versioned **job scorecard containing exactly 100 positive statements** describing the ideal occupant for that seat according to the seat's mission, responsibilities, expected outputs, quality focus, authority and boundaries.

Across 72 seats, one complete scorecard catalog therefore contains exactly **7,200 versioned statements**.

## Source of the statements

The 100 statements for a seat are a structured projection of that seat's canonical institutional contract. They must not be arbitrary personality questions or generic model benchmarks.

For example, Director scorecards are derived from the Director's documented mission, outputs and quality focus. Governance scorecards are derived from the Council/stage, Elder criterion, authority and prohibitions. The flow seat is derived from its procedural mission and boundaries.

A scorecard statement must express a characteristic, discipline, preference or way of working that is desirable for the occupant of that specific seat.

Statements are written positively so that `100% agreement` always represents stronger alignment with the ideal occupant. Version 1 does not use reverse-coded statements.

## Occupant perspective

An `OccupantCandidate` represents a concrete AI connection/model operating from the perspective of exactly one user/owner for that candidate.

The host may keep that user's identity private. Core only needs a stable opaque candidate identity and the candidate's scorecard responses.

The assessment prompt asks the connected AI to answer each statement **from the perspective of its user**. Conceptually:

> From the perspective of your user, what percentage do you agree with this statement?

Each answer is an integer or decimal percentage in the inclusive range `0..100`.

When the same human has multiple independently connected AI/model candidates, each candidate is evaluated independently because its behavior/context may differ.

## Assessment vector

For seat `S` and occupant candidate `C`, the assessment produces exactly 100 percentages:

```text
Assessment(S, C) = [p01, p02, ... p100]
where every p is in 0..100
```

The complete vector is evidence and must be preserved. Persisting only the final average is insufficient because the staffing decision must remain reconstructible.

The assessment may be obtained in one structured model invocation or in bounded batches. The contract does **not** require 100 separate external calls.

## Seat affinity formula

Version 1 gives every scorecard statement equal weight.

For a complete 100-answer vector:

```text
SeatAffinity(S, C) = (p01 + p02 + ... + p100) / 100
```

The result is a percentage in `0..100`.

There is no additional hidden weighting, model prestige multiplier, provider preference or user popularity factor in the Core v1 staffing score.

Operational availability determines whether a candidate can currently be used; it does not modify the stored scorecard affinity percentage.

## Staffing one seat

For a canonical seat, the Recruiter evaluates every eligible occupant candidate using the same version of that seat's 100-statement scorecard and selects the candidate with the highest `SeatAffinity`.

```text
Seat S
  x Candidate A -> 82.14%
  x Candidate B -> 91.37%  <- selected
  x Candidate C -> 76.52%
```

A candidate may win multiple or all seats. Core must not add an artificial diversity constraint that overrides `best person for the job`.

If two candidates have exactly the same affinity, the implementation must use a deterministic, documented tie-break that does not change their recorded affinity. The initial tie-break may use stable candidate identity/order rather than invent a second quality score.

## Complete 72-seat staffing snapshot

Institutional staffing evaluates the eligible occupant universe against all 72 scorecards and produces an immutable/versioned staffing snapshot containing, for each seat:

- seat identifier;
- scorecard identifier/version;
- selected occupant candidate identifier;
- selected model/deployment identifier when applicable;
- the 100 response percentages;
- final arithmetic-mean affinity;
- ranked alternative candidates and their affinities when retained by policy;
- assessment timestamp/version; and
- candidate-universe snapshot identifier.

The staffing snapshot is evidence. Later reevaluation must create a new snapshot rather than rewrite the one used by an earlier decision run.

## Reevaluation

When the candidate universe contains more than one AI occupant, a consuming product may periodically reassess candidates against the current scorecard catalog.

Core exposes the versioned assessment/staffing mechanism but does not prescribe whether reevaluation occurs weekly, monthly or on another bounded cadence.

A scorecard version change also requires a new assessment before scores from the new version are compared. Scores produced from different scorecard versions must not be silently mixed as if they were directly equivalent.

## Relationship to 12-of-25 Director recruitment

The 100-statement scorecards define **institutional seat staffing**: which AI occupant fills each of the 72 seats.

After staffing, the separate work-specific Recruiter stage selects 12 of the 25 already occupied Directors for a particular assignment.

The Director's staffing affinity remains part of the evidence for that occupied seat, but the 12-of-25 selection also depends on the Director role's relevance to the concrete work request. This document does not invent a separate work-relevance formula that has not yet been approved.

## Public/private boundary

Core owns:

- the 100-statement-per-seat scorecard contract;
- versioning;
- collection/validation of 0..100 responses;
- equal-weight arithmetic scoring;
- deterministic ranking;
- staffing snapshots; and
- historical reconstruction.

A consuming product owns:

- who is allowed into the occupant candidate universe;
- authentication/credentials for those AI connections;
- user/contact identity and relationship semantics;
- manual candidate scoping rules; and
- reevaluation cadence.

## Validation required before implementation is complete

Tests must demonstrate at least that:

1. exactly 72 scorecards exist and each contains exactly 100 statements;
2. all 7,200 statement identities are stable within their scorecard versions;
3. every response is constrained to `0..100`;
4. `SeatAffinity` is exactly the arithmetic mean of the 100 responses in v1;
5. the same scorecard version is used when comparing candidates for one seat/staffing run;
6. the highest-affinity eligible candidate is selected for each seat;
7. one candidate may validly win multiple or all 72 seats;
8. the complete 100-answer vector is preserved for reconstruction;
9. reevaluation creates a new immutable snapshot rather than rewriting historical evidence; and
10. no product-specific user identity, credential or relationship graph is required by the public scoring API.
