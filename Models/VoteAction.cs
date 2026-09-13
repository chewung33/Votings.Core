using System;

namespace Voting.Core.Models;

public abstract class VoteAction : VoteBase
{
    protected abstract void OnVotePassed();
    protected virtual void OnVoteFailed() { }
    protected virtual void OnVoteDraw() { }

    protected sealed override void OnCompleted()
    {
        switch (base.VoteResult.Result)
        {
            case Enums.VoteOutcome.Passed:
                OnVotePassed();
                break;

            case Enums.VoteOutcome.Failed:
                OnVoteFailed();
                break;

            case Enums.VoteOutcome.Draw:
                OnVoteDraw();
                break;
        }
    }
}