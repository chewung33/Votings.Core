using CommandSystem;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using System;
using Voting.Core.Features;

namespace Voting.Core.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
internal sealed class Vote : ICommand
{
    public string Command { get; } = "vote";
    public string[] Aliases { get; } = [];
    public string Description => string.Empty;

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count == 0)
        {
            response = Plugin.Instance.Config.Translation.VoteUsing;
            return false;
        }

        Player pl = Player.Get(sender);
        bool isYes = false;

        switch (arguments.At(0).ToLower())
        {
            case "yes":
                isYes = true;
                break;

            case "no":
                break;

            default:
                response = Plugin.Instance.Config.Translation.VoteUsing;
                return false;
        }

        if (VoteManager.CurrentVote is null || !VoteManager.CurrentVote.Vote(pl.UserId, isYes))
        {
            response = Plugin.Instance.Config.Translation.VoteError;
            return false;
        }

        response = Plugin.Instance.Config.Translation.Successfully;
        return true;
    }
}
