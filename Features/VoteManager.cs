using LabApi.Features.Wrappers;
using MEC;
using System.Collections.Generic;
using Voting.Core.Models;
using Voting.Core.Votings;

namespace Voting.Core.Features;

public static class VoteManager
{
    private static readonly List<VoteBase> Votes = [
        new VotingFriendlyFire() 
    ];

    public static void AddVote(VoteBase vote)
    {
        if (!Votes.Contains(vote))
        {
            Votes.Add(vote);
        }
    }

    internal static void StartVote(int index)
    {
        Votes[index]?.Start();
        Timing.RunCoroutine(Encounter(
            Votes[index],
            Votes[index].Duration == -1
            ? 15
            : Votes[index].Duration
        ));
    }

    internal static bool TryVote(int index, string steamId64, bool isYes) => Votes[index].Vote(steamId64, isYes);

    private static IEnumerator<float> Encounter(VoteBase voteBase, int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            string votingMessage = string.Format(
                Plugin.Instance.Config.Translation.VotingMessage,
                voteBase.Question,
                i,
                voteBase.VotedYes,
                voteBase.VotedNo
            );

            foreach (var pl in Player.ReadyList)
            {
                pl.SendBroadcast(votingMessage, 2, shouldClearPrevious: true);
            }

            yield return Timing.WaitForSeconds(1f);
        }

        CompleteVote(0);
        yield break;
    }

    private static void CompleteVote(int index)
    {
        Votes[index]?.Complete();
    }
}