using Voting.Core.Enums;

namespace Voting.Core.Models;

public sealed class VoteResult
{
    public uint VotedYes { get; }
    public uint VotedNo { get; }

    public uint Total => VotedYes + VotedNo;
    public VoteOutcome? Result { get; }

    public VoteResult(uint votedYes, uint votedNo)
    {
        VotedYes = votedYes;
        VotedNo = votedNo;

        Result = VotedYes.CompareTo(votedNo) switch
        {
            > 0 => VoteOutcome.Passed,
            < 0 => VoteOutcome.Failed,
            _ => VoteOutcome.Draw
        };
    }
}