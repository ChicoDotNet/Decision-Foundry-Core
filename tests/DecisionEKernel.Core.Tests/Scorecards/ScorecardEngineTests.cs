using DecisionEKernel.Core.Scorecards;
using Xunit;

namespace DecisionEKernel.Core.Tests.Scorecards;

public sealed class ScorecardEngineTests
{
    [Fact]
    public void Score_ComputesExactArithmeticMeanAndPreservesEvidence()
    {
        var scorecard = SyntheticScorecard("SYN-S01");
        var responses = Enumerable.Range(1, 100).Select(static value => (decimal)value).ToArray();

        var result = ScorecardEngine.Score(new CandidateAssessment("candidate-alpha", scorecard, responses));

        Assert.Equal(50.5m, result.Affinity);
        Assert.Equal(scorecard.CriterionIds, result.CriterionIds);
        Assert.Equal(responses, result.Responses);
    }

    [Fact]
    public void SelectBest_SelectsHighestAffinity()
    {
        var scorecard = SyntheticScorecard("SYN-S01");
        var winner = Assessment("candidate-b", scorecard, 91m);
        var other = Assessment("candidate-a", scorecard, 82m);

        Assert.Equal("candidate-b", ScorecardEngine.SelectBest([other, winner]).CandidateId);
    }

    [Fact]
    public void SelectBest_UsesOrdinalCandidateIdAsDeterministicTieBreak()
    {
        var scorecard = SyntheticScorecard("SYN-S01");

        var selected = ScorecardEngine.SelectBest([
            Assessment("candidate-z", scorecard, 90m),
            Assessment("candidate-a", scorecard, 90m)]);

        Assert.Equal("candidate-a", selected.CandidateId);
        Assert.Equal(90m, selected.Affinity);
    }

    [Fact]
    public void Score_RejectsIncompleteAndOutOfRangeVectors()
    {
        var scorecard = SyntheticScorecard("SYN-S01");

        Assert.Throws<ArgumentException>(() => ScorecardEngine.Score(
            new CandidateAssessment("candidate", scorecard, Enumerable.Repeat(50m, 99).ToArray())));

        var invalid = Enumerable.Repeat(50m, 100).ToArray();
        invalid[37] = 100.01m;
        Assert.Throws<ArgumentOutOfRangeException>(() => ScorecardEngine.Score(
            new CandidateAssessment("candidate", scorecard, invalid)));
    }

    [Fact]
    public void SelectBest_RejectsMixedPackIdentityOrCriterionOrdering()
    {
        var scorecard = SyntheticScorecard("SYN-S01");
        var otherVersion = scorecard with { Pack = scorecard.Pack with { Version = "synthetic-v2" } };
        var reordered = scorecard with { CriterionIds = scorecard.CriterionIds.Reverse().ToArray() };

        Assert.Throws<InvalidOperationException>(() => ScorecardEngine.SelectBest([
            Assessment("candidate-a", scorecard, 80m), Assessment("candidate-b", otherVersion, 90m)]));
        Assert.Throws<InvalidOperationException>(() => ScorecardEngine.SelectBest([
            Assessment("candidate-a", scorecard, 80m), Assessment("candidate-b", reordered, 90m)]));
    }

    [Fact]
    public void SyntheticPack_Contains72SeatsAnd7200UniqueOpaqueCriterionIds()
    {
        var definitions = Enumerable.Range(1, 72)
            .Select(index => SyntheticScorecard($"SYN-S{index:00}"))
            .ToArray();

        Assert.All(definitions, definition => Assert.Equal(100, definition.CriterionIds.Count));
        Assert.Equal(7200, definitions.SelectMany(static definition => definition.CriterionIds).Distinct(StringComparer.Ordinal).Count());
    }

    private static CandidateAssessment Assessment(string candidateId, ScorecardDefinition scorecard, decimal value) =>
        new(candidateId, scorecard, Enumerable.Repeat(value, 100).ToArray());

    private static ScorecardDefinition SyntheticScorecard(string seatId)
    {
        var pack = new ScorecardPackIdentity("synthetic-public-fixture", "synthetic-v1", "sha256:synthetic-not-production");
        var criterionIds = Enumerable.Range(1, 100).Select(index => $"{seatId}-C{index:000}").ToArray();
        return new ScorecardDefinition(seatId, pack, criterionIds);
    }
}
