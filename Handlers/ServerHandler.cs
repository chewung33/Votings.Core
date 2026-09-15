using LabApi.Events.CustomHandlers;
using Voting.Core.Features;
using Voting.Core.Votings;

namespace Voting.Core.Handlers;

internal sealed class ServerHandler : CustomEventsHandler
{
    public override void OnServerWaitingForPlayers()
    {
        if (Plugin.Instance.Config.VotingFF.IsEnabled)
        {
            VoteManager.StartVote<VotingFriendlyFire>();
        }
    }
}