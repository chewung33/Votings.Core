using System.Collections.Generic;

namespace Voting.Core.Models;

#nullable enable
public abstract class VoteBase
{
    private readonly Dictionary<string, bool> _voters = [];

    private bool _isStarted;
    private bool _isLocked;

    public abstract string Name { get; }
    public abstract string Question { get; }

    /// <summary>
    /// In seconds. If -1 in override class, then control by yourself. If you trying use VoteManager, then it`s set -1 to 30.
    /// </summary>
    public virtual short Duration { get; } = -1;

    public VoteResult? VoteResult { get; private set; }
    public uint VotedYes { get; private set; }
    public uint VotedNo { get; private set; }

    public bool Vote(string steamId64, bool isYes)
    {
        if (_isLocked || !_isStarted) return false;
        if (string.IsNullOrEmpty(steamId64)) return false;
        if (_voters.ContainsKey(steamId64)) return false;

        if (isYes) VotedYes++;
        else VotedNo++;

        _voters.Add(steamId64, isYes);
        return true;
    }

    public bool Start()
    {
        if (_isLocked) return false;

        _isStarted = true;
        OnVotingStarted();
        return true;
    }

    public bool Complete()
    {
        if (_isLocked) return false;

        _isLocked = true;
        VoteResult = GetResult();

        OnVotingEnded();
        OnCompleted();
        return true;
    }

    public void Refresh()
    {
        _voters.Clear();

        _isStarted = false;
        _isLocked = false;
        VoteResult = null;
        VotedYes = 0;
        VotedNo = 0;
    }

    protected virtual void OnVotingStarted() { }
    protected virtual void OnVotingEnded() { }

    /// <summary>
    /// Do not call directly.
    /// </summary>
    protected virtual void OnCompleted() { }

    private VoteResult GetResult() => new(VotedYes, VotedNo);
}