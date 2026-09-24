namespace DecisionEKernel.Core.Knowledge;

public readonly record struct KnowledgePackId(string Value);
public readonly record struct KnowledgePackVersion(string Value);
public readonly record struct KnowledgeDigest(string Value);
public readonly record struct ProviderId(string Value);
public readonly record struct CapabilityId(string Value);
public readonly record struct CriterionId(string Value);
public readonly record struct CapsuleId(string Value);

public sealed record KnowledgePackDescriptor(
    KnowledgePackId PackId,
    KnowledgePackVersion Version,
    KnowledgeDigest Digest,
    string ContractVersion,
    IReadOnlySet<CapabilityId> Capabilities,
    IReadOnlySet<CriterionId> Criteria);

public sealed record KnowledgeRequest(
    string WorkId,
    string SeatId,
    IReadOnlySet<CapabilityId> RequiredCapabilities,
    IReadOnlySet<CriterionId> RequiredCriteria,
    string ScopeHandle,
    string CorrelationId,
    int MaxResults = 8);

public sealed record CapabilityCapsule(
    CapsuleId CapsuleId,
    CapabilityId CapabilityId,
    string Version,
    string? RuntimeContext = null);

public sealed record KnowledgeResult(
    ProviderId ProviderId,
    KnowledgePackDescriptor Descriptor,
    IReadOnlyList<CapabilityCapsule> Capsules);

public interface IKnowledgeProvider
{
    ValueTask<KnowledgePackDescriptor> DescribeAsync(CancellationToken cancellationToken = default);
    ValueTask<KnowledgeResult> RetrieveAsync(KnowledgeRequest request, CancellationToken cancellationToken = default);
}

public sealed class KnowledgeProviderException : InvalidOperationException
{
    public KnowledgeProviderException(string message) : base(message) { }
}

/// <summary>Public deterministic provider for tests and examples only. Never substitutes for missing production knowledge.</summary>
public sealed class SyntheticKnowledgeProvider : IKnowledgeProvider
{
    private readonly ProviderId _providerId;
    private readonly KnowledgePackDescriptor _descriptor;
    private readonly IReadOnlyDictionary<CapabilityId, CapabilityCapsule> _capsules;

    public SyntheticKnowledgeProvider(ProviderId providerId, KnowledgePackDescriptor descriptor, IEnumerable<CapabilityCapsule> capsules)
    {
        _providerId = providerId;
        _descriptor = descriptor;
        _capsules = capsules.ToDictionary(x => x.CapabilityId);
    }

    public ValueTask<KnowledgePackDescriptor> DescribeAsync(CancellationToken cancellationToken = default)
        => ValueTask.FromResult(_descriptor);

    public ValueTask<KnowledgeResult> RetrieveAsync(KnowledgeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (string.IsNullOrWhiteSpace(request.ScopeHandle))
            throw new KnowledgeProviderException("An opaque authorization scope is required.");
        if (request.MaxResults <= 0)
            throw new KnowledgeProviderException("MaxResults must be positive.");

        var missingCriteria = request.RequiredCriteria.Where(x => !_descriptor.Criteria.Contains(x)).ToArray();
        if (missingCriteria.Length != 0)
            throw new KnowledgeProviderException("A required criterion is not present in the selected knowledge pack.");

        var missingCapabilities = request.RequiredCapabilities.Where(x => !_capsules.ContainsKey(x)).ToArray();
        if (missingCapabilities.Length != 0)
            throw new KnowledgeProviderException("A required capability cannot be resolved by the selected knowledge pack.");

        var capsules = request.RequiredCapabilities
            .Take(request.MaxResults)
            .Select(x => _capsules[x])
            .ToArray();

        return ValueTask.FromResult(new KnowledgeResult(_providerId, _descriptor, capsules));
    }
}
