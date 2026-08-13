# Canonical seat profile contract

- Status: Approved architecture direction; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core must not reduce the 72 canonical seats to names or generic tags. Every seat exists for a distinct institutional reason.

Core therefore requires an executable **seat profile** for each of the 72 canonical seats. A seat profile is the public, provider-neutral projection of the role contract needed to determine who is best suited to occupy that seat and how that occupied seat participates in the institutional flow.

The profile describes the **seat**, not its current occupant. Occupant/model characteristics are evaluated separately.

Every canonical seat profile owns a versioned [100-statement job scorecard](seat-scorecard-contract.md). The scorecard translates the role's documented reason for existence into exactly 100 positive statements that an occupant candidate can evaluate from the perspective of its user.

## Two distinct uses of seat profiles

Seat profiles participate in two different Recruiter decisions and those decisions must not be conflated.

### 1. Institutional staffing — all 72 seats

The Recruiter evaluates the available occupant/model candidates against **every one of the 72 canonical seat scorecards**.

For one candidate and one seat, the candidate returns 100 agreement percentages in `0..100`. Version 1 gives every statement equal weight and computes:

```text
SeatAffinity = arithmetic mean of 100 scorecard responses
```

The highest-affinity eligible candidate occupies the seat.

The result is an occupied 72-seat institution. The same occupant may fill many seats when it is the best available candidate for each of them. In the simplest community case, one AI connection may occupy all 72 seats.

This is the public expression of the principle **best person for the job**.

### 2. Work-specific Director recruitment — 12 of 25

Once the institution is staffed, a concrete work request does **not** rank all 72 occupied seats against each other.

The Recruiter evaluates the **25 occupied Director seats D01-D25** against the work request and selects exactly **12 Directors** for the domain cohort.

The Director's seat scorecard affinity is already known from staffing. The 12-of-25 stage adds work-specific domain relevance; it does not recompute institutional seat fit using a different hidden score.

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
- governance-stage metadata when the seat is a governance role;
- stable ordering/version metadata for compatibility; and
- a scorecard identifier/version resolving to exactly 100 positive statements.

A seat profile is not itself a prompt and must not contain product-private identity, tenant or credential information.

## Scorecard derivation rule

The scorecard must be derived from the canonical role semantics rather than invented as a generic personality survey.

Across the 72 seats, the catalog contains exactly **7,200 statements** in one complete version.

Each statement must describe an attribute, discipline, judgment tendency, working preference or responsibility alignment that is desirable for the ideal occupant of that specific role. Statements are expressed positively so stronger agreement always means stronger affinity.

The exact statement text is versioned. A material change to the 100 statements creates a new scorecard version and requires new occupant assessments before scores are compared under that version.

## Stable institutional semantics

Seat identities and their core role semantics must be versioned and backwards-compatible. Rewording documentation must not silently create a new seat identity.

If the public role contract changes materially, the change must be explicit and testable so a completed staffing or recruitment decision can reconstruct both the seat-profile and scorecard versions used at the time.

## Governance roles

Governance seats require additional metadata because their reason for existence is procedural rather than ordinary domain production.

At minimum a governance seat profile can express:

- governance stage;
- criterion or review responsibility;
- allowed decision/status outputs;
- veto/escalation boundaries; and
- prohibitions against changing underlying evidence or conclusions when applicable.

Their 100-statement scorecards must reflect those distinct responsibilities. Twenty-eight Elders are not interchangeable copies of one generic governance profile.

They receive occupants during institutional staffing but are **not** candidates for the 12-of-25 Director cohort. They activate when their governance stage applies.

## Flow role

A transversal flow seat such as a delivery/flow observer must be representable without pretending it is a domain-production role. Its profile and scorecard can declare procedural responsibilities, observable metrics and prohibitions against domain voting/opinion.

It receives an occupant through institutional staffing but is activated by its flow contract, not by Director recruitment.

## Recruitment explainability

The Recruiter must consume canonical seat profiles and their scorecards rather than infer role meaning from names.

For institutional staffing, its result must preserve the complete 100-response vector and arithmetic mean used to select an occupant for every seat.

For Director recruitment, its result must separately explain why each occupied Director entered or did not enter the 12-Director work cohort.

## Validation required before implementation is complete

Tests must demonstrate at least that:

1. all 72 canonical seats have a non-empty, versioned profile;
2. all seat IDs are unique and stable;
3. every seat profile declares a mission/reason for existence;
4. every seat resolves to exactly one versioned scorecard containing exactly 100 positive statements;
5. the catalog therefore contains exactly 7,200 statements per complete version;
6. institutional staffing can assign an occupant to all 72 seats using arithmetic-mean scorecard affinity;
7. the same occupant may validly occupy multiple or all seats;
8. Director recruitment evaluates only the 25 occupied Directors and selects exactly 12;
9. governance, synthesis, transversal and flow seats cannot consume one of the 12 Director slots; and
10. profile/scorecard changes that affect staffing/recruitment semantics are detectable and versioned.
