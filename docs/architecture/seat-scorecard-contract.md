# Canonical 100-criterion seat scorecard contract

- Status: Approved product architecture; revised for opaque knowledge boundary
- Decision owner: Alfonso Lara Ramos
- Date: 2026-09-23

## Purpose

Decision Foundry Core uses a deterministic, inspectable scorecard mechanism to measure how well an available AI occupant fits each of the 72 canonical seats.

The mechanism intentionally follows the recruiting philosophy **best person for the job**. It does not rely on a hidden composite heuristic for institutional staffing.

Each canonical seat is evaluated against exactly **100 versioned criterion identities**. A production knowledge pack may associate private human-readable semantics with those identities, but Core does not require that proprietary text to be committed or distributed publicly.

Across 72 seats, one complete scorecard pack therefore contains exactly:

```text
72 seats × 100 criterion identities = 7,200 criterion slots
```

## Public algorithm vs private semantics

Core owns the reusable scorecard algorithm and schema.

A host/provider may own the production semantics behind the criteria.

The canonical public shape is conceptually:

```text
Seat D07
ScorecardPack: enterprise-professional-v3
Criterion IDs: [D07-C001 ... D07-C100]
Assessment:    [87, 92, ... 74]
SeatAffinity = arithmetic mean(Assessment)
Pack digest = ...
```

Core can validate and score this structure without knowing what `D07-C037` means.

When human-readable criteria are needed to obtain an assessment, the authorized host/provider supplies them at runtime through the [Opaque knowledge provider contract](knowledge-provider-contract.md).

## Criterion source

Production criterion semantics should represent the seat's mission, responsibilities, expected outputs, quality focus, authority and boundaries.

They must not be arbitrary personality questions or generic prestige benchmarks.

The public contract does not mandate where production semantics are authored. They may be generated from private institutional knowledge, licensed material or another host-owned source.

A public sample may ship **synthetic** criterion text solely to prove the algorithm. Synthetic fixtures must be clearly separated from production packs and must not be derived from private curricula.

## Occupant perspective

An `OccupantCandidate` represents a concrete AI connection/model operating from the perspective of exactly one user/owner for that candidate.

The host may keep that user's identity private. Core only needs a stable opaque candidate identity, the applicable scorecard pack identity/version and the candidate's ordered response vector.

When a provider supplies human-readable criteria for assessment, the assessment process asks the connected AI to answer those criteria from the perspective required by the host. Core does not need to persist the private prompt text.

Each response is an integer or decimal percentage in the inclusive range `0..100`.

When the same human has multiple independently connected AI/model candidates, each candidate is evaluated independently because behavior/context may differ.

## Assessment vector

For seat `S`, occupant candidate `C` and one immutable scorecard pack version `P`:

```text
CriterionIds(P,S) = [c01, c02, ... c100]
Assessment(P,S,C) = [p01, p02, ... p100]
where every p is in 0..100
```

The complete ordered vector and its criterion IDs are evidence and must be preserved. Persisting only the final average is insufficient because the staffing decision must remain reconstructible.

The assessment may be obtained in one structured model invocation or in bounded batches. The contract does **not** require 100 separate external calls.

## Seat affinity formula

Version 1 gives every criterion equal weight.

For a complete 100-answer vector:

```text
SeatAffinity(P,S,C) = (p01 + p02 + ... + p100) / 100
```

The result is a percentage in `0..100`.

There is no additional hidden weighting, model prestige multiplier, provider preference or user popularity factor in the Core v1 staffing score.

Operational availability determines whether a candidate can currently be used; it does not modify the stored scorecard affinity percentage.

## Staffing one seat

For a canonical seat, the Recruiter evaluates every eligible occupant candidate using the same scorecard-pack version and selects the candidate with the highest `SeatAffinity`.

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
- scorecard-pack ID/version/digest;
- ordered 100 criterion IDs;
- selected occupant candidate identifier;
- selected model/deployment identifier when applicable;
- ordered 100 response percentages;
- final arithmetic-mean affinity;
- ranked alternative candidates and their affinities when retained by policy;
- assessment timestamp/version; and
- candidate-universe snapshot identifier.

The staffing snapshot is evidence. Later reevaluation must create a new snapshot rather than rewrite the one used by an earlier decision run.

Core does not require the snapshot to duplicate human-readable private criterion text.

## Reevaluation

When the candidate universe contains more than one AI occupant, a consuming product may periodically reassess candidates against the current scorecard pack.

Core exposes the versioned assessment/staffing mechanism but does not prescribe whether reevaluation occurs weekly, monthly or on another bounded cadence.

A pack/version change requires a new assessment before scores from the new version are compared. Scores produced from different pack versions must not be silently mixed as if they were directly equivalent.

## Relationship to 12-of-25 Director recruitment

The 100-criterion scorecards define **institutional seat staffing**: which AI occupant fills each of the 72 seats.

After staffing, the separate work-specific Recruiter stage selects 12 of the 25 already occupied Directors for a particular assignment.

The Director's staffing affinity remains part of the evidence for that occupied seat, but the 12-of-25 selection also depends on the Director role's relevance to the concrete work request.

## Public/private boundary

Core owns:

- the exactly-100-criteria-per-seat contract;
- stable criterion-identity shape;
- pack/version/digest compatibility rules;
- validation of ordered `0..100` response vectors;
- equal-weight arithmetic scoring;
- deterministic ranking;
- staffing snapshots;
- reconstruction semantics; and
- synthetic fixtures/tests needed to prove the public algorithm.

Core does **not** require public distribution of:

- production human-readable criterion text;
- private curricula/source documents;
- source-repository paths;
- proprietary derivation/provenance;
- production embeddings/vector indexes; or
- private capability capsules.

A consuming host/provider owns those concerns as defined by the [Opaque knowledge provider contract](knowledge-provider-contract.md), together with candidate authentication/credentials, private identity semantics and reevaluation policy.

## Validation required before implementation is complete

Tests must demonstrate at least that:

1. exactly 72 scorecard definitions are addressable and each resolves exactly 100 stable criterion identities for one pack version;
2. all 7,200 criterion identities are unique/stable within that pack version;
3. every response is constrained to `0..100`;
4. `SeatAffinity` is exactly the arithmetic mean of the 100 responses in v1;
5. the same pack/version/digest is used when comparing candidates for one seat/staffing run;
6. the highest-affinity eligible candidate is selected for each seat;
7. one candidate may validly win multiple or all 72 seats;
8. the complete criterion-ID ordering and 100-answer vector are preserved for reconstruction;
9. reevaluation creates a new immutable snapshot rather than rewriting historical evidence;
10. Core can run these tests with a synthetic public fixture pack;
11. Core does not require production criterion text in source/package assets; and
12. no product-specific user identity, credential, relationship graph or private corpus is required by the public scoring API.
