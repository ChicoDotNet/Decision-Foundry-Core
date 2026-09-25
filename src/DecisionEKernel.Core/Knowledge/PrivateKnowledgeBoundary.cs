namespace DecisionEKernel.Core.Knowledge;

/// <summary>
/// Public boundary validator for opaque knowledge-plane metadata. It deliberately
/// knows nothing about production statements, embeddings, provenance or private corpus content.
/// </summary>
public static class PrivateKnowledgeBoundary
{
    public static void ValidateDescriptor(KnowledgePackDescriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);
        RequireOpaque(descriptor.PackId.Value, nameof(descriptor.PackId));
        RequireOpaque(descriptor.Version.Value, nameof(descriptor.Version));
        RequireDigest(descriptor.Digest.Value);
        RequireOpaque(descriptor.ContractVersion, nameof(descriptor.ContractVersion));

        foreach (var capability in descriptor.Capabilities)
            RequireOpaque(capability.Value, nameof(descriptor.Capabilities));
        foreach (var criterion in descriptor.Criteria)
            RequireOpaque(criterion.Value, nameof(descriptor.Criteria));
    }

    public static void ValidateResult(KnowledgeRequest request, KnowledgeResult result)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(result);
        ValidateDescriptor(result.Descriptor);
        RequireOpaque(result.ProviderId.Value, nameof(result.ProviderId));

        if (result.Capsules.Count > request.MaxResults)
            throw new KnowledgeProviderException("Knowledge provider exceeded MaxResults.");

        var required = request.RequiredCapabilities;
        foreach (var capsule in result.Capsules)
        {
            RequireOpaque(capsule.CapsuleId.Value, nameof(capsule.CapsuleId));
            RequireOpaque(capsule.CapabilityId.Value, nameof(capsule.CapabilityId));
            RequireOpaque(capsule.Version, nameof(capsule.Version));
            if (!required.Contains(capsule.CapabilityId))
                throw new KnowledgeProviderException("Knowledge provider returned an unrequested capability.");
        }
    }

    private static void RequireOpaque(string value, string field)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 128 || value.Any(char.IsWhiteSpace))
            throw new KnowledgeProviderException($"{field} must be a compact opaque identifier.");
    }

    private static void RequireDigest(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 16 || value.Length > 128 ||
            value.Any(c => !char.IsAsciiHexDigit(c)))
            throw new KnowledgeProviderException("Knowledge digest must be an opaque hexadecimal digest.");
    }
}
