using DecisionEKernel.Core.Knowledge;

namespace DecisionEKernel.Core.Tests.Knowledge;

public sealed class PrivateKnowledgeBoundaryTests
{
    [Fact]
    public void Descriptor_with_only_opaque_metadata_is_valid()
    {
        var descriptor = Descriptor();
        PrivateKnowledgeBoundary.ValidateDescriptor(descriptor);
    }

    [Fact]
    public void Human_readable_identifier_is_rejected()
    {
        var descriptor = Descriptor() with { PackId = new KnowledgePackId("private production knowledge") };
        Assert.Throws<KnowledgeProviderException>(() => PrivateKnowledgeBoundary.ValidateDescriptor(descriptor));
    }

    [Fact]
    public void Non_hex_digest_is_rejected()
    {
        var descriptor = Descriptor() with { Digest = new KnowledgeDigest("this-is-not-a-digest") };
        Assert.Throws<KnowledgeProviderException>(() => PrivateKnowledgeBoundary.ValidateDescriptor(descriptor));
    }

    [Fact]
    public void Provider_cannot_return_unrequested_capability()
    {
        var request = new KnowledgeRequest(
            "work-001", "seat-001", new HashSet<CapabilityId> { new("cap-001") },
            new HashSet<CriterionId> { new("crit-001") }, "scope-001", "corr-001", 8);
        var result = new KnowledgeResult(
            new ProviderId("provider-001"), Descriptor(),
            new[] { new CapabilityCapsule(new CapsuleId("capsule-002"), new CapabilityId("cap-002"), "v1") });

        Assert.Throws<KnowledgeProviderException>(() => PrivateKnowledgeBoundary.ValidateResult(request, result));
    }

    [Fact]
    public void Provider_cannot_exceed_request_cardinality()
    {
        var request = new KnowledgeRequest(
            "work-001", "seat-001", new HashSet<CapabilityId> { new("cap-001"), new("cap-002") },
            new HashSet<CriterionId> { new("crit-001") }, "scope-001", "corr-001", 1);
        var result = new KnowledgeResult(
            new ProviderId("provider-001"), Descriptor(),
            new[]
            {
                new CapabilityCapsule(new CapsuleId("capsule-001"), new CapabilityId("cap-001"), "v1"),
                new CapabilityCapsule(new CapsuleId("capsule-002"), new CapabilityId("cap-002"), "v1")
            });

        Assert.Throws<KnowledgeProviderException>(() => PrivateKnowledgeBoundary.ValidateResult(request, result));
    }

    private static KnowledgePackDescriptor Descriptor() => new(
        new KnowledgePackId("pack-001"), new KnowledgePackVersion("v1"),
        new KnowledgeDigest("0123456789abcdef0123456789abcdef"), "contract-v1",
        new HashSet<CapabilityId> { new("cap-001"), new("cap-002") },
        new HashSet<CriterionId> { new("crit-001") });
}
