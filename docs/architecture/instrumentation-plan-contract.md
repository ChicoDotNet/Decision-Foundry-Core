# Instrumentation Plan contract

- Status: Approved architecture direction; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-08-13

## Purpose

Decision Foundry Core needs a reusable contract that connects **what work should be performed** with **which staffed Director assignments should perform it**.

An `InstrumentationPlan` is the case/work-request description of the professional instrumentarium selected for execution.

It prevents the runtime from reducing work allocation to simple title matching or asking all 25 Directors to participate in every case.

## Public mental model

```text
Work request
    |
    v
Decisive questions
    |
    v
Instrumentation Plan
  - artifact / deliverable references
  - lineage-lens references
  - evidence requirements
  - required expertise
  - dependencies / quality gates
    |
    v
25 staffed Director assignments
    |
    v
12 recruited Directors
```

Core owns the reusable shape and orchestration semantics. A consuming host may own richer private catalogs, identities, candidate-source rules or commercial policy.

## Artifact references

Core does not require every professional artifact definition to live inside the runtime assembly.

An Instrumentation Plan must be able to reference one or more versioned artifacts/deliverables that the host wants produced or evaluated.

Each selected artifact reference should carry or resolve to enough metadata for the runtime to understand equivalents of:

- stable artifact identity and version;
- decisive questions answered;
- required inputs/evidence;
- expected outputs;
- dependencies;
- applicability rationale;
- quality checks; and
- required expertise/capabilities.

The plan must distinguish a **selected/requested artifact** from a **completed/validated artifact**.

## Lineage-lens references

A plan may associate one or more generic **lineage lens references** with an artifact or work package.

Core treats a lens as metadata describing a useful professional/industry perspective. It does not impersonate, instantiate or assume the identity of the referenced source.

Exact lineage catalog content belongs to the host/project documentation. The public contract only requires stable identity/version/provenance/restriction metadata when a host uses lenses.

Multiple lenses may inform the same artifact.

## Required expertise

The plan derives or carries required expertise from the selected questions/artifacts/work packages.

That expertise is used during work-specific Director recruitment.

The matching unit is conceptually:

```text
Director seat profile
      +
current occupant assignment
      +
Instrumentation Plan required work/expertise
      =
DirectorWorkAffinity
```

The public engine must not reduce this to Director-name keyword matching.

## Relationship to institutional staffing

Instrumentation planning does not replace Stage 1 Recruiter staffing.

The intended order is:

1. produce or reuse the current complete **72-seat staffing snapshot**;
2. build/refine the Instrumentation Plan for the work request;
3. evaluate the **25 occupied Director seats D01-D25** against the plan;
4. select exactly **12 distinct Director seats with their current occupants**.

Explicit seat bindings remain part of the staffing snapshot and are respected during Director recruitment.

## Co-optimization and refinement

The plan and recruited expertise may refine one another during framing.

A host/runtime may therefore support an initial planning pass followed by bounded refinement:

```text
candidate artifacts / questions / lenses
              <---- refinement ---->
recruited Director expertise
```

Examples of legitimate refinement include:

- a Director identifies a missing evidence requirement;
- an artifact dependency makes another domain relevant;
- an artifact is removed because it no longer reduces uncertainty;
- a new contradiction requires an additional work package; or
- a planned artifact becomes not applicable after evidence arrives.

Every material plan revision must be versioned/reconstructible.

## Minimal public contract

Exact type names remain an implementation decision, but Core requires equivalents of:

- `WorkRequestId` / case-work identity;
- plan version and timestamp;
- problem/decision descriptor;
- decisive-question references;
- selected artifact/work-package references with versions;
- per-selection rationale;
- optional lineage-lens references;
- evidence requirements/gaps;
- required expertise/capability descriptors;
- dependency information;
- quality/gate requirements;
- 72-seat staffing snapshot reference;
- exactly 12 recruited Director seat+occupant assignments after recruitment;
- plan revision/provenance information.

The plan must be serializable and suitable for persistence/audit by a host.

## Non-Director roles

The 12-Director cohort is only the domain-production cohort.

The Instrumentation Plan may declare or imply required non-Director activations, but those roles do not compete for Director slots.

Examples include:

- synthesis roles;
- governance/review stages;
- transversal capabilities; and
- flow/delivery observation.

Their exact public execution contract remains defined by the runtime/institution contracts.

## Public/private boundary

Core must not require private concepts such as:

- Enterprise tenant/user identity;
- social-contact relationships;
- private consulting strategy or commercial packaging;
- private candidate-source rules;
- private pricing/spend calculations; or
- proprietary artifact content.

A host may supply those concerns through generic references, candidate sets and extension points.

## Reconstruction

A completed run should be able to reconstruct:

- which plan version was used;
- which artifacts/work packages were selected, deferred or not applicable;
- which evidence requirements were known;
- which lineage lenses were referenced when applicable;
- which staffing snapshot was active;
- which 12 Director seat+occupant assignments were recruited; and
- which plan revisions occurred before completion.

Later catalog, staffing or policy changes must not silently rewrite the plan evidence of a completed run.

## Validation required before implementation is complete

Tests must demonstrate at least that:

1. a work request can produce or consume a versioned Instrumentation Plan;
2. selected artifact/work references carry explicit rationale and question/expertise relationships;
3. lineage-lens references remain metadata rather than executable identities;
4. multiple lenses can be associated with one work item when the host supplies them;
5. Director recruitment evaluates only the 25 staffed Directors;
6. exactly 12 distinct Director seats are selected for a valid work cohort;
7. Director selection consumes plan-required expertise rather than only seat-name matching;
8. explicit seat bindings remain preserved in the selected Director assignments;
9. plan refinements create new versions without mutating completed history; and
10. Core remains independently usable without Enterprise identity, tenancy or private policy types.