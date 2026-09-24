using DecisionEKernel.Core.Knowledge;

namespace DecisionEKernel.Core.Tests.Knowledge;

public sealed class SyntheticKnowledgeProviderTests
{
    private static readonly CapabilityId CapabilityA = new("cap.synthetic.a");
    private static readonly CapabilityId CapabilityB = new("cap.synthetic.b");
    private static readonly CriterionId CriterionA = new("criterion.synthetic.001");

    [Fact]
    public async Task DescribeAsync_ReturnsOpaqueDescriptor()
    {
        var (provider, descriptor) = CreateProvider();

        var actual = await provider.DescribeAsync();

        Assert.Equal(descriptor, actual);
        Assert.Equal("1.0", actual.ContractVersion);
    }

    [Fact]
    public async Task RetrieveAsync_ReturnsOnlyRequestedCapabilities()
    {
        var (provider, descriptor) = CreateProvider();
        var request = Request([CapabilityB], [CriterionA]);

        var result = await provider.RetrieveAsync(request);

        Assert.Equal(new ProviderId("provider.synthetic"), result.ProviderId);
        Assert.Equal(descriptor, result.Descriptor);
        Assert.Collection(result.Capsules, capsule => Assert.Equal(CapabilityB, capsule.CapabilityId));
    }

    [Fact]
    public async Task RetrieveAsync_RespectsMaxResults()
    {
        var (provider, _) = CreateProvider();
        var request = Request([CapabilityA, CapabilityB], [CriterionA], maxResults: 1);

        var result = await provider.RetrieveAsync(request);

        Assert.Single(result.Capsules);
    }

    [Fact]
    public async Task RetrieveAsync_FailsClosedWithoutScope()
    {
        var (provider, _) = CreateProvider();
        var request = Request([CapabilityA], [CriterionA], scopeHandle: " ");

        await Assert.ThrowsAsync<KnowledgeProviderException>(async () => await provider.RetrieveAsync(request));
    }

    [Fact]
    public async Task RetrieveAsync_FailsClosedForUnknownCriterion()
    {
        var (provider, _) = CreateProvider();
        var request = Request([CapabilityA], [new CriterionId("criterion.synthetic.999")]);

        await Assert.ThrowsAsync<KnowledgeProviderException>(async () => await provider.RetrieveAsync(request));
    }

    [Fact]
    public async Task RetrieveAsync_FailsClosedForUnknownCapability()
    {
        var (provider, _) = CreateProvider();
        var request = Request([new CapabilityId("cap.synthetic.missing")], [CriterionA]);

        await Assert.ThrowsAsync<KnowledgeProviderException>(async () => await provider.RetrieveAsync(request));
    }

    [Fact]
    public async Task RetrieveAsync_RejectsNonPositiveMaxResults()
    {
        var (provider, _) = CreateProvider();
        var request = Request([CapabilityA], [CriterionA], maxResults: 0);

        await Assert.ThrowsAsync<KnowledgeProviderException>(async () => await provider.RetrieveAsync(request));
    }

    private static (SyntheticKnowledgeProvider Provider, KnowledgePackDescriptor Descriptor) CreateProvider()
    {
        var descriptor = new KnowledgePackDescriptor(
            new KnowledgePackId("pack.synthetic"),
            new KnowledgePackVersion("1.0.0"),
            new KnowledgeDigest(new string('a', 64)),
            "1.0",
            new HashSet<CapabilityId> { CapabilityA, CapabilityB },
            new HashSet<CriterionId> { CriterionA });

        var capsules = new[]
        {
            new CapabilityCapsule(new CapsuleId("capsule.synthetic.a"), CapabilityA, "1.0.0"),
            new CapabilityCapsule(new CapsuleId("capsule.synthetic.b"), CapabilityB, "1.0.0")
        };

        return (new SyntheticKnowledgeProvider(new ProviderId("provider.synthetic"), descriptor, capsules), descriptor);
    }

    private static KnowledgeRequest Request(
        IReadOnlySet<CapabilityId> capabilities,
        IReadOnlySet<CriterionId> criteria,
        string scopeHandle = "scope.synthetic",
        int maxResults = 8) =>
        new("work.synthetic", "seat.synthetic", capabilities, criteria, scopeHandle, "correlation.synthetic", maxResults);
}
