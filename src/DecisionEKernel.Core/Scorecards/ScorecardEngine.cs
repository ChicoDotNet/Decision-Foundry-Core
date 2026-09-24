namespace DecisionEKernel.Core.Scorecards;

public sealed record ScorecardPackIdentity(string PackId, string Version, string Digest);

public sealed record ScorecardDefinition(
    string SeatId,
    ScorecardPackIdentity Pack,
    IReadOnlyList<string> CriterionIds);

public sealed record CandidateAssessment(
    string CandidateId,
    ScorecardDefinition Scorecard,
    IReadOnlyList<decimal> Responses);

public sealed record ScoredCandidate(
    string CandidateId,
    ScorecardPackIdentity Pack,
    string SeatId,
    IReadOnlyList<string> CriterionIds,
    IReadOnlyList<decimal> Responses,
    decimal Affinity);

public static class ScorecardEngine
{
    public const int CriteriaPerSeat = 100;

    public static ScoredCandidate Score(CandidateAssessment assessment)
    {
        ArgumentNullException.ThrowIfNull(assessment);
        ValidateDefinition(assessment.Scorecard);

        if (string.IsNullOrWhiteSpace(assessment.CandidateId))
        {
            throw new ArgumentException("CandidateId is required.", nameof(assessment));
        }

        if (assessment.Responses.Count != CriteriaPerSeat)
        {
            throw new ArgumentException($"Exactly {CriteriaPerSeat} responses are required.", nameof(assessment));
        }

        if (assessment.Responses.Any(static response => response is < 0m or > 100m))
        {
            throw new ArgumentOutOfRangeException(nameof(assessment), "Every response must be in the inclusive range 0..100.");
        }

        var affinity = assessment.Responses.Sum() / CriteriaPerSeat;

        return new ScoredCandidate(
            assessment.CandidateId,
            assessment.Scorecard.Pack,
            assessment.Scorecard.SeatId,
            assessment.Scorecard.CriterionIds.ToArray(),
            assessment.Responses.ToArray(),
            affinity);
    }

    public static ScoredCandidate SelectBest(IEnumerable<CandidateAssessment> assessments)
    {
        ArgumentNullException.ThrowIfNull(assessments);
        var scored = assessments.Select(Score).ToArray();

        if (scored.Length == 0)
        {
            throw new ArgumentException("At least one candidate assessment is required.", nameof(assessments));
        }

        var first = scored[0];
        if (scored.Any(candidate => candidate.SeatId != first.SeatId || candidate.Pack != first.Pack || !candidate.CriterionIds.SequenceEqual(first.CriterionIds)))
        {
            throw new InvalidOperationException("Candidates may only be compared using the same seat, pack/version/digest and ordered criterion IDs.");
        }

        return scored
            .OrderByDescending(static candidate => candidate.Affinity)
            .ThenBy(static candidate => candidate.CandidateId, StringComparer.Ordinal)
            .First();
    }

    public static void ValidateDefinition(ScorecardDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(definition.Pack);
        ArgumentNullException.ThrowIfNull(definition.CriterionIds);

        if (string.IsNullOrWhiteSpace(definition.SeatId) ||
            string.IsNullOrWhiteSpace(definition.Pack.PackId) ||
            string.IsNullOrWhiteSpace(definition.Pack.Version) ||
            string.IsNullOrWhiteSpace(definition.Pack.Digest))
        {
            throw new ArgumentException("SeatId and complete pack identity are required.", nameof(definition));
        }

        if (definition.CriterionIds.Count != CriteriaPerSeat)
        {
            throw new ArgumentException($"Exactly {CriteriaPerSeat} criterion IDs are required.", nameof(definition));
        }

        if (definition.CriterionIds.Any(string.IsNullOrWhiteSpace) ||
            definition.CriterionIds.Distinct(StringComparer.Ordinal).Count() != CriteriaPerSeat)
        {
            throw new ArgumentException("Criterion IDs must be non-empty and unique within the scorecard.", nameof(definition));
        }
    }
}
