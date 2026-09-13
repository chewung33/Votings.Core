using LabApi.Events.CustomHandlers;
using Voting.Core.Features;

namespace Voting.Core.Handlers;

internal sealed class ServerHandler : CustomEventsHandler
{
    public override void OnServerWaitingForPlayers()
    {
        if (Plugin.Instance.Config.VotingFF.IsEnabled)
        {
            VoteManager.StartVote(0);
        }
    }
}