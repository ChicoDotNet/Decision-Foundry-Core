using DecisionEKernel.Core.Instrumentation;
using DecisionEKernel.Core.Staffing;
using Xunit;

namespace DecisionEKernel.Core.Tests.Instrumentation;

public sealed class InstrumentationPlanTests
{
    [Fact]
    public void Recruitment_UsesOpaqueCapabilities_AndSelectsExactlyTwelveStaffedDirectors()
    {
        var plan = CreatePlan();
        var profiles = Enumerable.Range(1, 25)
            .Select(i => new DirectorCapabilityProfile(
                new StaffedSeat($"D{i:00}", $"candidate-{i:00}", 80m, i == 1 ? SelectionProvenance.ExplicitBinding : SelectionProvenance.AutomaticRecruiter),
                i <= 12 ? new HashSet<string> { "cap-001", "cap-002" } : new HashSet<string> { "cap-001" }))
            .ToArray();

        var recruited = InstrumentationPlanner.Recruit(plan, profiles);

        Assert.Equal(12, recruited.RecruitedDirectors.Count);
        Assert.Equal(12, recruited.RecruitedDirectors.Select(d => d.SeatId).Distinct(StringComparer.Ordinal).Count());
        Assert.All(recruited.RecruitedDirectors, d => Assert.Contains(d.SeatId, Enumerable.Range(1, 12).Select(i => $"D{i:00}")));
        Assert.Equal(SelectionProvenance.ExplicitBinding, recruited.RecruitedDirectors.Single(d => d.SeatId == "D01").Provenance);
    }

    [Fact]
    public void Recruitment_IsDeterministic_WhenCapabilityCoverageTies()
    {
        var plan = CreatePlan();
        var profiles = Enumerable.Range(1, 25)
            .Reverse()
            .Select(i => new DirectorCapabilityProfile(
                new StaffedSeat($"D{i:00}", $"candidate-{i:00}", 50m, SelectionProvenance.AutomaticRecruiter),
                new HashSet<string> { "cap-001" }))
            .ToArray();

        var recruited = InstrumentationPlanner.Recruit(plan, profiles);

        Assert.Equal(Enumerable.Range(1, 12).Select(i => $"D{i:00}"), recruited.RecruitedDirectors.Select(d => d.SeatId));
    }

    [Fact]
    public void Revision_CreatesNewVersion_WithoutMutatingPriorPlan()
    {
        var original = CreatePlan() with
        {
            RecruitedDirectors = new[] { new StaffedSeat("D01", "candidate-01", 100m, SelectionProvenance.ExplicitBinding) }
        };

        var revised = original.Revise("2", "evidence-gap-001", new DateTimeOffset(2026, 9, 25, 2, 0, 0, TimeSpan.Zero));

        Assert.Equal("1", original.Version);
        Assert.Single(original.RecruitedDirectors);
        Assert.Equal("2", revised.Version);
        Assert.Empty(revised.RecruitedDirectors);
        Assert.Equal(new PlanRevision("1", "evidence-gap-001"), revised.Revision);
    }

    [Fact]
    public void ArtifactSelection_RequiresExplicitRationale()
    {
        var invalid = CreatePlan() with
        {
            Artifacts = new[] { new ArtifactReference("artifact-001", "1", "", new[] { "question-001" }, new[] { "cap-001" }) }
        };
        var profiles = Enumerable.Range(1, 25)
            .Select(i => new DirectorCapabilityProfile(
                new StaffedSeat($"D{i:00}", $"candidate-{i:00}", 50m, SelectionProvenance.AutomaticRecruiter),
                new HashSet<string> { "cap-001" }))
            .ToArray();

        Assert.Throws<ArgumentException>(() => InstrumentationPlanner.Recruit(invalid, profiles));
    }

    private static InstrumentationPlan CreatePlan() => new(
        "work-001",
        "1",
        new DateTimeOffset(2026, 9, 25, 1, 0, 0, TimeSpan.Zero),
        "synthetic-decision-001",
        new[] { "question-001" },
        new[] { new ArtifactReference("artifact-001", "1", "rationale-001", new[] { "question-001" }, new[] { "cap-001", "cap-002" }) },
        new[] { new LineageLensReference("lens-001", "1"), new LineageLensReference("lens-002", "1") },
        new[] { new EvidenceRequirement("evidence-001", false) },
        new HashSet<string> { "cap-001", "cap-002" },
        "staffing-snapshot-001",
        Array.Empty<StaffedSeat>(),
        null);
}