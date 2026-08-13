# Explicit seat binding contract

- Status: Approved product architecture; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core supports both automatic `best person for the job` staffing and explicit host/user control over who occupies a canonical seat.

When a host has more than one eligible AI/connector candidate, it may explicitly bind a chosen candidate to one or more of the 72 canonical seats instead of allowing the Recruiter to choose automatically for those seats.

This is a public Core capability. Core expresses the choice in provider-neutral terms such as seat + concrete occupant candidate/connector. A consuming product may present the same operation with richer product language, such as assigning a team member to a role.

## Staffing precedence

For every canonical seat, staffing follows this precedence:

```text
Explicit seat binding exists?
        |
        +-- yes --> use the explicitly bound eligible occupant
        |
        +-- no  --> Recruiter selects highest scorecard affinity
```

An explicit binding therefore overrides automatic staffing **for that seat only**.

The Recruiter continues to staff every unbound seat automatically using the canonical 100-statement scorecard and the eligible candidate universe.

## Supported modes

A Core host may use any of these modes:

1. **Fully automatic** — no explicit bindings; Recruiter staffs all 72 seats.
2. **Fully explicit** — host supplies an occupant binding for every seat.
3. **Mixed** — host pins selected seats and Recruiter staffs the remainder.

All three modes preserve the same 72 canonical seat identities and role contracts.

## Binding unit

The public binding unit is conceptually:

```text
CanonicalSeatId
        +
Concrete OccupantCandidate
        =
ExplicitSeatBinding
```

An `OccupantCandidate` identifies the concrete selectable AI endpoint used for execution, not merely a provider family name. Depending on the connector abstraction it may include equivalents of:

- connection identity;
- provider/family identity;
- concrete model/version identity;
- deployment identity when applicable; and
- opaque owner/perspective identity supplied by the host.

This allows different model generations or deployments from the same provider to occupy different seats.

For example, a Core host may explicitly choose:

```text
Council/Elder seats -> candidate using model/version A
Director seats      -> candidate using model/version B
```

or the reverse. Core does not privilege a newer/larger model merely because it is newer/larger; explicit binding is a valid host decision.

If several models/deployments are available behind one account/connection, they remain independently selectable candidates when the host exposes them that way.

## Eligibility still applies

An explicit binding is not permission to bypass basic validity or authorization.

The bound candidate must still:

- belong to the host-supplied eligible candidate universe;
- resolve to a usable connector when the seat executes; and
- satisfy structural compatibility required by the public connector contract.

Core must reject or surface an invalid explicit binding rather than silently substituting another candidate.

Operational unavailability may prevent execution, but it must not silently convert a historical explicit assignment into an automatic one without an explicit fallback policy.

## Scorecard evidence and manual choice

A host may assess an explicitly bound candidate against the seat's 100-statement scorecard even when the automatic Recruiter is not choosing that seat.

When such an assessment exists, Core preserves the affinity as evidence but **does not override the explicit choice merely because another candidate scored higher**.

This distinction must remain visible:

- `SelectedBy = AutomaticRecruiter`; or
- `SelectedBy = ExplicitBinding`.

An explicit binding is therefore a governance/user choice, not a falsified claim that the bound candidate was the highest-scoring occupant.

## Binding lifetime

Exact persistence policy belongs to the host, but the public contract must allow bindings to be scoped and versioned.

At minimum a host must be able to distinguish:

- a binding for one execution/request;
- a binding intended to remain current until changed; and
- the historical binding snapshot used by a completed run.

Changing a current binding must never rewrite the binding evidence of an earlier completed run.

## Interaction with 12-of-25 Director recruitment

Explicit seat binding controls **who occupies a seat**. It does not, by itself, force an occupied Director into the 12-of-25 work cohort.

For example, a host may explicitly bind Candidate A to D17. D17 still competes with the other 24 occupied Directors for work-specific inclusion unless the host separately supplies an explicit Director-cohort policy allowed by the runtime contract.

Thus:

```text
Seat binding = who occupies D17
Director recruitment = whether D17 is one of the 12 needed for this work
```

These decisions must not be conflated.

## Public/private boundary

Core knows only:

- seat identity;
- concrete candidate/connector/model/deployment identity;
- explicit-vs-automatic selection provenance;
- optional scorecard assessment evidence; and
- binding scope/version.

Core does not need to know whether the host presents the candidate as:

- a person;
- an enterprise team member;
- a social contact;
- a service account; or
- a model/provider connection.

Those semantics belong to the consuming product.

## Validation required before implementation is complete

Tests must demonstrate at least that:

1. an explicit eligible binding wins over automatic Recruiter selection for that seat;
2. unbound seats continue to use automatic scorecard staffing;
3. fully automatic, fully explicit and mixed staffing are all valid;
4. different versions/deployments of one provider can be exposed as independent candidates and bound to different seats;
5. an explicit candidate outside the eligible universe is rejected rather than silently accepted;
6. scorecard affinity may be preserved for an explicitly bound occupant without changing the explicit choice;
7. selection provenance distinguishes automatic from explicit staffing;
8. changing a current binding does not rewrite completed-run history; and
9. explicit seat binding does not automatically force a Director into the 12-of-25 work cohort.
