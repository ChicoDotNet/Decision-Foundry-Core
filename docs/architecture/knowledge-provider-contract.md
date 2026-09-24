# Opaque knowledge provider contract

- Status: Approved architecture direction; not yet implemented
- Decision owner: Alfonso Lara Ramos
- Date: 2026-09-23

## Purpose

Decision Foundry Core is public engine code. Production knowledge may be proprietary, confidential or licensed under terms that do not permit redistribution.

Core therefore consumes institutional knowledge through a provider boundary instead of requiring production knowledge text, embeddings, curriculum provenance or derived private artifacts to live in the public repository or package.

The design goal is:

```text
public engine
    +
opaque versioned knowledge references
    +
private host/provider
    =
private executable institutional capability
```

The provider boundary protects the distinction between **how the engine reasons/orchestrates** and **the private knowledge used at runtime**.

## Core ownership

Core owns reusable mechanics:

- stable seat identities;
- scorecard and capability schemas;
- deterministic validation;
- scoring mathematics;
- staffing and recruitment algorithms;
- orchestration contracts;
- instrumentation and reconstruction contracts;
- opaque identifiers, digests and version references;
- provider-neutral request/response shapes; and
- synthetic fixtures required to test the public package independently.

Core does **not** require the public repository or NuGet/package payload to contain production proprietary knowledge.

## Host/provider ownership

A consuming host or provider may own:

- human-readable production scorecard criteria;
- proprietary curricula and source documents;
- capability capsules derived from private sources;
- embeddings and vector indexes;
- private semantic-search implementation;
- private source provenance;
- access-control rules;
- organization/private memory;
- licensed third-party knowledge; and
- policies governing which knowledge may be retrieved for a request.

The provider may be implemented in-process, out-of-process or across a private service boundary.

## Stable opaque identities

Core must be able to operate on stable identifiers without understanding the private semantics behind them.

Equivalent public contract types should include:

- `KnowledgePackId`;
- `KnowledgePackVersion`;
- `KnowledgeArtifactId`;
- `CapabilityId`;
- `CriterionId`;
- `AuthorityId`;
- `KnowledgeDigest`; and
- `ProviderId`.

Identifiers must be stable within a versioned pack. Human-readable descriptions are optional runtime payloads and must not be required to compile, test or distribute Core.

## Knowledge-pack descriptor

A provider exposes a descriptor sufficient for compatibility and reconstruction without exposing source content.

A descriptor should include equivalents of:

- pack ID and semantic/version identifier;
- schema version;
- creation/build timestamp;
- content digest;
- compatible Core contract version;
- available seat/capability/criterion identities;
- optional feature/capability declarations; and
- provider provenance that does not reveal private source material.

The descriptor itself must not require curriculum paths, source text or embeddings.

## Retrieval request

A runtime knowledge request should carry the smallest useful request shape, for example:

- work/case identity;
- requesting seat/role;
- required capability IDs;
- optional criterion/authority IDs;
- bounded query or task descriptor;
- requested evidence/provenance level;
- freshness requirement;
- maximum result count/context budget;
- opaque authorization/scope handle supplied by the host; and
- correlation/trace identifier.

Core must not invent authorization. The provider decides whether the caller may retrieve a requested capability.

## Retrieval result

A provider may return one or more **capability capsules** or references.

A capsule is a runtime-safe derived artifact that may contain equivalents of:

- capsule ID and version;
- capability/authority IDs;
- concise decision rules or operating constraints;
- required inputs;
- expected outputs;
- hard gates;
- conditions and exceptions;
- counterexamples/falsifiers;
- required artifacts/evidence;
- confidence/freshness metadata;
- private source digest/reference that is meaningful only to the provider; and
- optional runtime context text intended for the authorized private execution.

Core treats capsule payloads as runtime data. It must not persist or publish them unless the host explicitly configures persistence under its own policy.

A provider may alternatively return an opaque content handle for a private host to resolve immediately before model invocation.

## Original-source drill-down

Production deployments may support two retrieval levels:

1. **Derived capability retrieval** — preferred default; returns capability capsules or equivalent bounded derived knowledge.
2. **Original-source retrieval** — optional privileged path; retrieves source excerpts only when policy explicitly authorizes them.

Core must not require original-source access for normal operation.

## Embeddings and indexes

Embeddings are implementation details of a knowledge provider, not public Core assets.

Core:

- does not prescribe a vector database;
- does not require embeddings to be serializable through the public API;
- does not publish production embeddings;
- does not treat embeddings as encryption; and
- does not assume semantic search is the only valid retrieval implementation.

A deterministic/in-memory provider can satisfy the same contract for tests.

## Scorecard relationship

The canonical staffing contract still requires exactly 100 criterion responses per seat and deterministic arithmetic-mean affinity.

However, the production **criterion semantics/text** may live in a private knowledge pack.

Core therefore requires:

- exactly 100 stable `CriterionId` values for each seat in one scorecard-pack version;
- an ordered 100-value assessment vector tied to those IDs;
- pack ID/version/digest;
- deterministic validation and scoring; and
- immutable reconstruction evidence.

Core does not require production criterion text to be committed publicly.

A public sample may ship a clearly synthetic fixture pack whose contents are not derived from proprietary curricula.

## No-leak invariants

A conforming public Core release must be testable for the following:

1. building Core does not require access to a proprietary repository;
2. published package contents do not contain production curriculum/source text;
3. published package contents do not contain production embeddings/vector indexes;
4. production criterion descriptions are not required public constants/resources;
5. private repository paths and source provenance are not required runtime metadata;
6. logs and instrumentation identify pack/capsule IDs and digests rather than dumping private payloads by default;
7. serialization snapshots redact or omit private capsule payloads unless explicitly enabled by the host;
8. a synthetic provider can exercise all public algorithms; and
9. replacing one provider/pack with another does not require changing Core algorithms.

## Reconstruction and audit

A completed run should preserve enough non-secret evidence to reconstruct what contract was used:

- provider ID;
- knowledge pack ID/version/digest;
- capability/capsule IDs used;
- criterion IDs and assessment vector where applicable;
- retrieval request metadata safe to retain;
- policy/authorization decision reference when supplied by the host; and
- execution trace correlation.

The host decides whether private payloads or original excerpts are retained. Core's default contract favors identifiers and digests over content duplication.

## Failure behavior

Core must fail closed when:

- a required pack cannot be resolved;
- pack/schema compatibility is invalid;
- a required criterion/capability ID is missing;
- a provider denies authorization;
- a digest/version changes unexpectedly within one immutable run; or
- a required retrieval result cannot be obtained.

Core must not silently substitute public demo knowledge for missing production knowledge.

## Validation required before implementation is complete

Tests must demonstrate at least that:

1. the same Core algorithms run with both a synthetic in-memory provider and a private-provider test double;
2. scorecard math consumes opaque criterion identities plus assessment vectors without requiring criterion text in Core;
3. pack/version/digest mismatches fail closed;
4. unauthorized retrieval fails closed;
5. missing required capability IDs fail closed;
6. default tracing does not emit private capsule payloads;
7. a runtime can reconstruct which pack/capsule identities influenced a run;
8. embeddings/vector-store implementation remains outside Core;
9. public package inspection finds no configured production proprietary corpus; and
10. production knowledge can be replaced or re-versioned without recompiling Core.
