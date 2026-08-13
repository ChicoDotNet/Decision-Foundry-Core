# Canonical seat profile contract

- Status: Approved architecture direction; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core must not reduce the 72 canonical seats to names or generic tags. Every seat exists for a distinct institutional reason.

Core therefore requires an executable **seat profile** for each of the 72 canonical seats. A seat profile is the public, provider-neutral projection of the role contract needed to determine who is best suited to occupy that seat and how that occupied seat participates in the institutional flow.

The profile describes the **seat**, not its current occupant. Occupant/model characteristics are evaluated separately.

## Two distinct uses of seat profiles

Seat profiles participate in two different Recruiter decisions and those decisions must not be conflated.

### 1. Institutional staffing — all 72 seats

The Recruiter may evaluate the available occupant/model candidates against **every one of the 72 canonical seat profiles**.

The goal is to assign the best available occupant to each seat:

```text
SeatProfile
    +
OccupantProfile
    =
SeatOccupancyAffinity
```

The result is an occupied 72-seat institution. The same occupant may fill many seats when it is the best available candidate for each of them. In the simplest community case, one AI connection may occupy all 72 seats.

This is the public expression of the principle **best person for the job**.

### 2. Work-specific Director recruitment — 12 of 25

Once the institution is staffed, a concrete work request does **not** rank all 72 occupied seats against each other.

The Recruiter evaluates the **25 occupied Director seats D01-D25** against the work request and selects exactly **12 Directors** for the domain cohort.

```text
DirectorSeatProfile
    +
CurrentOccupantProfile
    +
WorkRequest
    =
DirectorWorkAffinity
```

The remaining institutional seats are activated according to their structural, synthesis, governance, transversal or flow contracts rather than competing for a Director slot.

## Required seat-profile semantics

Exact public type names remain an implementation decision, but every canonical seat definition must carry equivalents of:

- stable seat identifier;
- role/display name;
- institutional layer and grouping;
- mission / reason for existence;
- responsibilities or questions the seat is accountable for;
- expected inputs;
- expected outputs;
- quality focus / validation criteria;
- authority granted to the role;
- explicit prohibitions or boundaries;
- relevant capabilities/modalities expected from an occupant;
- governance-stage metadata when the seat is a governance role; and
- stable ordering/version metadata for compatibility.

A seat profile is not a prompt and must not contain product-private identity, tenant or credential information.

## Stable institutional semantics

Seat identities and their core role semantics must be versioned and backwards-compatible. Rewording documentation must not silently create a new seat identity.

If the public role contract changes materially, the change must be explicit and testable so a completed staffing or recruitment decision can reconstruct the seat-profile version used at the time.

## Governance roles

Governance seats require additional metadata because their reason for existence is procedural rather than ordinary domain production.

At minimum a governance seat profile can express:

- governance stage;
- criterion or review responsibility;
- allowed decision/status outputs;
- veto/escalation boundaries; and
- prohibitions against changing underlying evidence or conclusions when applicable.

They receive occupants during institutional staffing but are **not** candidates for the 12-of-25 Director cohort. They activate when their governance stage applies.

## Flow role

A transversal flow seat such as a delivery/flow observer must be representable without pretending it is a domain-production role. Its profile can declare procedural responsibilities, observable metrics and prohibitions against domain voting/opinion.

It receives an occupant through institutional staffing but is activated by its flow contract, not by Director recruitment.

## Recruitment explainability

The Recruiter must consume canonical seat profiles rather than infer role meaning from names.

For institutional staffing, its result must be inspectable enough to explain why a particular occupant was selected for each seat.

For Director recruitment, its result must separately explain why each occupied Director entered or did not enter the 12-Director work cohort.

## Validation required before implementation is complete

Tests must demonstrate at least that:

1. all 72 canonical seats have a non-empty, versioned profile;
2. all seat IDs are unique and stable;
3. every seat profile declares a mission/reason for existence;
4. institutional staffing can assign an occupant to all 72 seats;
5. the same occupant may validly occupy multiple or all seats;
6. Director recruitment evaluates only the 25 occupied Directors and selects exactly 12;
7. governance, synthesis, transversal and flow seats cannot consume one of the 12 Director slots;
8. recruitment uses profile content rather than seat-name string matching; and
9. seat-profile changes that affect staffing/recruitment semantics are detectable and versioned.
