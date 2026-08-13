# Canonical seat profile contract

- Status: Approved architecture direction; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core must not reduce the 72 canonical seats to names or generic tags. Every seat exists for a distinct institutional reason and recruitment depends on that distinction.

Core therefore requires an executable **seat profile** for each of the 72 canonical seats. A seat profile is the public, provider-neutral projection of the role contract needed to determine whether that seat belongs in a work cohort.

The profile describes the **seat**, not its current occupant. Occupant/model/user-connection characteristics are evaluated separately and combined with seat fit by the Recruiter.

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
- relevant capabilities/modalities needed from an occupant;
- governance-stage metadata when the seat is a governance role; and
- stable ordering/version metadata for compatibility.

A seat profile is not a prompt and must not contain product-private identity, tenant or credential information.

## Seat fit and occupant fit are different dimensions

Recruitment evaluates two independent dimensions before forming an agent assignment:

```text
SeatFit(work, canonical seat profile)
        +
OccupantFit(work, occupant/model profile)
        =
AssignmentAffinity(work, seat + occupant)
```

`SeatFit` asks whether the institutional role is appropriate for the requested work.

`OccupantFit` asks whether the available model/connector occupant is appropriate for performing that role for the requested work.

A strong model in an irrelevant seat is not automatically a strong assignment. Likewise, the correct seat paired with an incapable or unavailable occupant is not a strong assignment.

## Stable institutional semantics

Seat identities and their core role semantics must be versioned and backwards-compatible. Rewording documentation must not silently create a new seat identity.

If the public role contract changes materially, the change must be explicit and testable so a completed recruitment decision can reconstruct the seat profile version used at the time.

## Governance roles

Governance seats require additional metadata because their reason for existence is procedural rather than ordinary domain production.

At minimum a governance seat profile can express:

- governance stage;
- criterion or review responsibility;
- allowed decision/status outputs;
- veto/escalation boundaries; and
- prohibitions against changing underlying evidence or conclusions when applicable.

Core remains neutral to product-private identity and candidate sourcing while preserving the distinct institutional role of those seats.

## Flow role

A transversal flow seat such as a delivery/flow observer must be representable without pretending it is a domain-production role. Its seat profile can declare procedural responsibilities, observable metrics and prohibitions against domain voting/opinion.

## Recruitment consequence

The Recruiter must consume canonical seat profiles rather than infer role meaning from names.

For a fixed work request and occupant set, its result must be inspectable enough to explain:

- why a seat was considered relevant or irrelevant;
- why an occupant was considered appropriate or inappropriate for that seat; and
- why the resulting seat/occupant assignment entered or did not enter the selected 12-agent cohort.

## Validation required before implementation is complete

Tests must demonstrate at least that:

1. all 72 canonical seats have a non-empty, versioned profile;
2. all seat IDs are unique and stable;
3. every seat profile declares a mission/reason for existence;
4. domain, transversal, governance and flow seats can express distinct contracts without special-casing product identity;
5. recruitment uses profile content rather than seat-name string matching; and
6. seat-profile changes that affect recruitment semantics are detectable/versioned.
