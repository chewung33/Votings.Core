using LabApi.Features.Wrappers;
using MEC;
using System.Collections.Generic;
using System.Linq;
using Voting.Core.Models;
using Voting.Core.Votings;

namespace Voting.Core.Features;

#nullable enable
public static class VoteManager
{
    private static readonly List<VoteBase> Votes = [
        new VotingFriendlyFire()
    ];

    public static VoteBase? CurrentVote { get; private set; }

    public static bool IsActive => CurrentVote is not null;

    public static void AddVoteBase(VoteBase vote)
    {
        if (!Votes.Contains(vote))
        {
            Votes.Add(vote);
        }
    }

    public static void RemoveVoteBase<T>() where T : VoteBase
    {
        if (TryGetVoteBase<T>(out VoteBase voteBase))
        {
            Votes.Remove(voteBase);
        }
    }

    public static IEnumerable<string> GetAvailableNamesVoteBases()
    {
        foreach (var item in Votes)
        {
            yield return item.Name;
        }
    }

    public static bool TryGetVoteBase<T>(out VoteBase voteBase) where T : VoteBase
    {
        voteBase = GetVoteBase<T>();

        return voteBase is not null;
    }

    public static bool TryGetVoteBaseByName(string name, out VoteBase voteBase)
    {
        voteBase = GetVoteBaseByName(name);

        return voteBase is not null;
    }

    public static void StartVote<T>() where T : VoteBase
    {
        if (IsActive) return;
        if (!TryGetVoteBase<T>(out VoteBase voteBase)) return;

        voteBase.Start();
        Timing.RunCoroutine(Encounter(
            voteBase,
            voteBase.Duration == -1
            ? 30
            : voteBase.Duration
        ));
    }

    public static bool TryVote<T>(string steamId64, bool isYes) where T : VoteBase
    {
        if (TryGetVoteBase<T>(out VoteBase voteBase))
        {
            voteBase.Vote(steamId64, isYes);
            return true;
        }

        return false;
    }

    private static IEnumerator<float> Encounter(VoteBase voteBase, int seconds)
    {
        CurrentVote = voteBase;

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

        voteBase.Complete();
        voteBase.Refresh();
        CurrentVote = null;
        yield break;
    }

    private static VoteBase GetVoteBase<T>() where T : VoteBase
    {
        return Votes.FirstOrDefault(r => r.GetType() == typeof(T));
    }

    private static VoteBase GetVoteBaseByName(string name)
    {
        return Votes.FirstOrDefault(r => r.Name == name);
    }
}