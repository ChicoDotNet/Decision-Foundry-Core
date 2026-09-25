using DecisionEKernel.Core.Staffing;

namespace DecisionEKernel.Core.Instrumentation;

public sealed record ArtifactReference(string ArtifactId, string Version, string Rationale, IReadOnlyList<string> QuestionIds, IReadOnlyList<string> RequiredCapabilityIds);
public sealed record LineageLensReference(string LensId, string Version);
public sealed record EvidenceRequirement(string EvidenceId, bool IsSatisfied);
public sealed record DirectorCapabilityProfile(StaffedSeat Assignment, IReadOnlySet<string> CapabilityIds);
public sealed record PlanRevision(string PreviousVersion, string Reason);

public sealed record InstrumentationPlan(
    string WorkRequestId,
    string Version,
    DateTimeOffset CreatedAt,
    string DecisionDescriptor,
    IReadOnlyList<string> DecisiveQuestionIds,
    IReadOnlyList<ArtifactReference> Artifacts,
    IReadOnlyList<LineageLensReference> LineageLenses,
    IReadOnlyList<EvidenceRequirement> EvidenceRequirements,
    IReadOnlySet<string> RequiredCapabilityIds,
    string StaffingSnapshotId,
    IReadOnlyList<StaffedSeat> RecruitedDirectors,
    PlanRevision? Revision)
{
    public InstrumentationPlan Revise(string newVersion, string reason, DateTimeOffset createdAt) =>
        this with
        {
            Version = Require(newVersion, nameof(newVersion)),
            CreatedAt = createdAt,
            RecruitedDirectors = Array.Empty<StaffedSeat>(),
            Revision = new PlanRevision(Version, Require(reason, nameof(reason)))
        };

    private static string Require(string value, string parameterName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
        return value;
    }
}

public static class InstrumentationPlanner
{
    public static InstrumentationPlan Recruit(
        InstrumentationPlan plan,
        IReadOnlyCollection<DirectorCapabilityProfile> staffedDirectors)
    {
        ArgumentNullException.ThrowIfNull(plan);
        ArgumentNullException.ThrowIfNull(staffedDirectors);
        ValidatePlan(plan);
        if (staffedDirectors.Count != 25)
            throw new ArgumentException("Recruitment requires exactly 25 staffed Director assignments.", nameof(staffedDirectors));

        var workAffinities = staffedDirectors.Select(profile =>
        {
            ArgumentNullException.ThrowIfNull(profile.Assignment);
            ArgumentNullException.ThrowIfNull(profile.CapabilityIds);
            var affinity = plan.RequiredCapabilityIds.Count == 0
                ? 0m
                : 100m * plan.RequiredCapabilityIds.Count(profile.CapabilityIds.Contains) / plan.RequiredCapabilityIds.Count;
            return new DirectorWorkAffinity(profile.Assignment.SeatId, profile.Assignment.CandidateId, affinity);
        }).ToArray();

        var recruited = StaffingEngine.RecruitDirectors(workAffinities)
            .Select(selected => staffedDirectors.Single(p => StringComparer.Ordinal.Equals(p.Assignment.SeatId, selected.SeatId)).Assignment)
            .ToArray();

        return plan with { RecruitedDirectors = recruited };
    }

    private static void ValidatePlan(InstrumentationPlan plan)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(plan.WorkRequestId);
        ArgumentException.ThrowIfNullOrWhiteSpace(plan.Version);
        ArgumentException.ThrowIfNullOrWhiteSpace(plan.DecisionDescriptor);
        ArgumentException.ThrowIfNullOrWhiteSpace(plan.StaffingSnapshotId);
        ArgumentNullException.ThrowIfNull(plan.RequiredCapabilityIds);
        ArgumentNullException.ThrowIfNull(plan.Artifacts);
        if (plan.Artifacts.Any(a => string.IsNullOrWhiteSpace(a.Rationale)))
            throw new ArgumentException("Every selected artifact requires explicit rationale.", nameof(plan));
    }
}