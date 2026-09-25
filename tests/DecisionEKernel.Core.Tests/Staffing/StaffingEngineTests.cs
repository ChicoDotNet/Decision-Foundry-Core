using DecisionEKernel.Core.Staffing;
using Xunit;

namespace DecisionEKernel.Core.Tests.Staffing;

public sealed class StaffingEngineTests
{
    [Fact]
    public void Explicit_binding_wins_over_higher_affinity_candidate()
    {
        CandidateAffinity[] candidates = [new("candidate-a", 99m), new("candidate-b", 61m)];
        var result = StaffingEngine.Staff("D01", candidates, new SeatBinding("D01", "candidate-b"));

        Assert.Equal("candidate-b", result.CandidateId);
        Assert.Equal(61m, result.Affinity);
        Assert.Equal(SelectionProvenance.ExplicitBinding, result.Provenance);
    }

    [Fact]
    public void Automatic_staffing_selects_highest_affinity_with_ordinal_tie_break()
    {
        CandidateAffinity[] candidates = [new("candidate-z", 88m), new("candidate-a", 88m), new("candidate-m", 70m)];
        var result = StaffingEngine.Staff("D02", candidates);

        Assert.Equal("candidate-a", result.CandidateId);
        Assert.Equal(SelectionProvenance.AutomaticRecruiter, result.Provenance);
    }

    [Fact]
    public void Same_candidate_can_fill_multiple_seats()
    {
        CandidateAffinity[] candidates = [new("best", 95m), new("other", 80m)];
        var first = StaffingEngine.Staff("D01", candidates);
        var second = StaffingEngine.Staff("D02", candidates);

        Assert.Equal("best", first.CandidateId);
        Assert.Equal("best", second.CandidateId);
    }

    [Fact]
    public void Recruitment_selects_exactly_twelve_distinct_director_seats()
    {
        var directors = Enumerable.Range(1, 25)
            .Select(i => new DirectorWorkAffinity($"D{i:00}", $"candidate-{i:00}", i))
            .ToArray();

        var cohort = StaffingEngine.RecruitDirectors(directors);

        Assert.Equal(12, cohort.Count);
        Assert.Equal(12, cohort.Select(x => x.SeatId).Distinct(StringComparer.Ordinal).Count());
        Assert.Equal("D25", cohort[0].SeatId);
        Assert.Equal("D14", cohort[^1].SeatId);
    }

    [Fact]
    public void Non_director_role_cannot_consume_a_director_slot()
    {
        var directors = Enumerable.Range(1, 24)
            .Select(i => new DirectorWorkAffinity($"D{i:00}", $"candidate-{i:00}", i))
            .Append(new DirectorWorkAffinity("VP01", "candidate-vp", 100m))
            .ToArray();

        Assert.Throws<ArgumentException>(() => StaffingEngine.RecruitDirectors(directors));
    }

    [Fact]
    public void Recruitment_is_deterministic_on_equal_work_affinity()
    {
        var directors = Enumerable.Range(1, 25)
            .Select(i => new DirectorWorkAffinity($"D{i:00}", $"candidate-{i:00}", 50m))
            .Reverse()
            .ToArray();

        var cohort = StaffingEngine.RecruitDirectors(directors);

        Assert.Equal(Enumerable.Range(1, 12).Select(i => $"D{i:00}"), cohort.Select(x => x.SeatId));
    }
}
