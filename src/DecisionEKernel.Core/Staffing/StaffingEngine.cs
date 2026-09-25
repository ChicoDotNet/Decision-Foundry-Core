namespace DecisionEKernel.Core.Staffing;

public enum SelectionProvenance { ExplicitBinding, AutomaticRecruiter }

public sealed record CandidateAffinity(string CandidateId, decimal Affinity);
public sealed record SeatBinding(string SeatId, string CandidateId);
public sealed record StaffedSeat(string SeatId, string CandidateId, decimal Affinity, SelectionProvenance Provenance);
public sealed record DirectorWorkAffinity(string SeatId, string CandidateId, decimal Affinity);

public static class StaffingEngine
{
    public static StaffedSeat Staff(
        string seatId,
        IReadOnlyCollection<CandidateAffinity> candidates,
        SeatBinding? explicitBinding = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(seatId);
        ArgumentNullException.ThrowIfNull(candidates);
        if (candidates.Count == 0) throw new ArgumentException("At least one eligible candidate is required.", nameof(candidates));

        foreach (var candidate in candidates)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(candidate.CandidateId);
            if (candidate.Affinity is < 0m or > 100m)
                throw new ArgumentOutOfRangeException(nameof(candidates), "Affinity must be in the inclusive range 0..100.");
        }

        if (explicitBinding is not null)
        {
            if (!StringComparer.Ordinal.Equals(explicitBinding.SeatId, seatId))
                throw new ArgumentException("Explicit binding must target the seat being staffed.", nameof(explicitBinding));

            var bound = candidates.SingleOrDefault(c => StringComparer.Ordinal.Equals(c.CandidateId, explicitBinding.CandidateId))
                ?? throw new ArgumentException("Explicit binding candidate must be eligible.", nameof(explicitBinding));
            return new StaffedSeat(seatId, bound.CandidateId, bound.Affinity, SelectionProvenance.ExplicitBinding);
        }

        var selected = candidates
            .OrderByDescending(c => c.Affinity)
            .ThenBy(c => c.CandidateId, StringComparer.Ordinal)
            .First();
        return new StaffedSeat(seatId, selected.CandidateId, selected.Affinity, SelectionProvenance.AutomaticRecruiter);
    }

    public static IReadOnlyList<DirectorWorkAffinity> RecruitDirectors(
        IReadOnlyCollection<DirectorWorkAffinity> staffedDirectors,
        int cohortSize = 12)
    {
        ArgumentNullException.ThrowIfNull(staffedDirectors);
        if (staffedDirectors.Count != 25)
            throw new ArgumentException("Work-specific recruitment requires exactly 25 staffed Director seats.", nameof(staffedDirectors));
        if (cohortSize != 12)
            throw new ArgumentOutOfRangeException(nameof(cohortSize), "The v1 Director cohort size is exactly 12.");
        if (staffedDirectors.Select(d => d.SeatId).Distinct(StringComparer.Ordinal).Count() != 25)
            throw new ArgumentException("Director seat identities must be distinct.", nameof(staffedDirectors));
        if (staffedDirectors.Any(d => !IsDirectorSeat(d.SeatId)))
            throw new ArgumentException("Only canonical Director seats D01-D25 may compete for the cohort.", nameof(staffedDirectors));
        if (staffedDirectors.Any(d => d.Affinity is < 0m or > 100m))
            throw new ArgumentOutOfRangeException(nameof(staffedDirectors), "Work affinity must be in the inclusive range 0..100.");

        return staffedDirectors
            .OrderByDescending(d => d.Affinity)
            .ThenBy(d => d.SeatId, StringComparer.Ordinal)
            .Take(cohortSize)
            .ToArray();
    }

    private static bool IsDirectorSeat(string seatId) =>
        seatId is { Length: 3 } && seatId[0] == 'D' && int.TryParse(seatId.AsSpan(1), out var number) && number is >= 1 and <= 25;
}
