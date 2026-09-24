# Candidate eligibility and preferred-model contract

- Status: Approved architecture direction; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core keeps **job affinity** separate from **candidate eligibility/default-selection policy**.

The canonical 100-statement scorecard answers how well an occupant candidate fits a seat. A host may independently decide which concrete candidates are eligible to compete or be selected explicitly based on budget, licensing, policy, availability or user preference.

Core therefore exposes generic hooks for host-supplied candidate eligibility and preferred/default candidate metadata without knowing the product-specific reason behind them.

## Separation of concerns

The intended ordering is:

```text
Host candidate source
        |
        v
Host eligibility/default policy
        |
        v
Eligible concrete OccupantCandidates
        |
        v
100-statement SeatAffinity
        |
        +-- automatic staffing
        |
        +-- explicit seat binding
```

Eligibility determines whether a candidate may participate.

Affinity remains the equal-weight arithmetic mean of the seat's 100 assessment responses and must not be altered by budget/preference metadata.

## Generic metadata

Exact public type names remain an implementation decision, but Core requires equivalents of:

- stable concrete candidate identity;
- optional preferred/default-candidate flag or rank;
- eligibility decision/status;
- optional normalized expected-cost value;
- optional generic maximum-allowed-cost constraint;
- policy/snapshot version used to determine eligibility; and
- reason/provenance metadata suitable for reconstruction without exposing private host identity semantics.

Core does not require cost metadata when a host does not use cost-based eligibility.

## Preferred/default candidate

A host may designate one candidate as the default or preferred candidate within a logical candidate group.

Core does not define how the host derives that preference. A host might use recent usage, explicit user choice, a local default, availability or another documented policy.

A preferred candidate is not automatically higher-affinity. Preference is configuration/UX metadata, not a scorecard bonus.

## Cost ceiling

A host may provide normalized expected-cost metadata and a maximum allowed cost.

When such a policy is active:

```text
candidate expected cost <= maximum allowed cost
    -> candidate may remain eligible

candidate expected cost > maximum allowed cost
    -> candidate is ineligible under that policy snapshot
```

The host may also designate a default/preferred candidate as eligible through its own policy even when that candidate's cost relationship differs from the generic alternative-candidate ceiling. Core consumes the host's final eligibility decision rather than re-deriving product policy.

Core does not define the currency, billing model, averaging window, usage window or cost-normalization formula. Those belong to the host.

A host may omit cost policy entirely.

## Interaction with automatic staffing

Automatic Recruiter staffing considers only candidates marked eligible under the active host policy.

Among eligible candidates, the winner is still determined by the canonical seat-affinity rule. Cost or default preference must not be multiplied into the 100-statement affinity percentage.

## Interaction with explicit seat binding

An explicit seat binding may select any concrete candidate that remains eligible under the active policy snapshot.

A preferred/default candidate may be preselected by a UI, but the host may allow the user to choose another eligible candidate.

The staffing snapshot preserves the concrete candidate actually used and the active eligibility/policy version.

## Reconstruction

A completed staffing/binding snapshot must be able to reconstruct:

- candidate identity;
- eligibility result;
- policy snapshot/version;
- preferred/default status when applicable;
- normalized expected cost and ceiling when supplied;
- independent seat-affinity score; and
- final automatic/explicit selection provenance.

A later policy update must not change historical eligibility evidence.

## Public/private boundary

Core must not infer or persist product-specific concepts such as:

- Enterprise user usage/spend history;
- social-contact budgets;
- salary, seniority or purchasing authority;
- commercial plan limits; or
- why a particular host designated one model as preferred.

Those remain host concerns.

## Validation required before implementation is complete

Tests must demonstrate at least that:

1. an ineligible candidate cannot win automatic staffing;
2. an ineligible candidate cannot be explicitly bound without a new/changed host policy;
3. preferred/default status does not alter SeatAffinity;
4. a generic cost ceiling can remove an otherwise high-affinity candidate from the eligible set;
5. among remaining eligible candidates, the highest seat affinity still wins automatically;
6. explicit binding may choose any eligible non-default candidate;
7. the host may explicitly keep a preferred/default candidate eligible under its own versioned policy;
8. hosts may omit cost policy entirely; and
9. completed-run policy/eligibility evidence remains immutable.
