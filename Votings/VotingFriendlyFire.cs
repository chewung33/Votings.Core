using LabApi.Features.Wrappers;
using Voting.Core.Models;

namespace Voting.Core.Votings;

internal sealed class VotingFriendlyFire : VoteAction
{
    private bool _previousStateFriendlyFire;

    public override string Name { get; } = "Friendly Fire";
    public override string Question => Plugin.Instance?.Config.Translation.VotingFF.Question;
    public override short Duration { get; } = 30;

    protected override void OnVotingStarted()
    {
        _previousStateFriendlyFire = Server.FriendlyFire;
        Round.IsLocked = true;
    }
    protected override void OnVotingEnded()
    {
        Round.IsLocked = false;

        string resultMessage = string.Format(
            Plugin.Instance.Config.Translation.ResultOfTheVotingMessage,
            this.VoteResult.Result == Enums.VoteOutcome.Passed
            ? Plugin.Instance?.Config.Translation.Enabled
            : Plugin.Instance?.Config.Translation.Disabled,
            this.VoteResult.VotedYes,
            this.VoteResult.VotedNo
        );

        foreach (var pl in Player.ReadyList)
        {
            pl.SendBroadcast(resultMessage, 5, shouldClearPrevious: true);
        }
    }

    protected override void OnVotePassed()
    {
        Server.FriendlyFire = true;
    }

    protected override void OnVoteDraw()
    {
        Server.FriendlyFire = _previousStateFriendlyFire;
    }

    protected override void OnVoteFailed()
    {
        Server.FriendlyFire = false;
    }
}