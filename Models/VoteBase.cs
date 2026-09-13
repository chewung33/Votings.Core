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
    /// In seconds. If -1 in override class, then control by yourself.
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

    public void Start()
    {
        _isStarted = true;
        OnVotingStarted();
    }

    public void Complete()
    {
        if (_isLocked) return;

        _isLocked = true;
        VoteResult = GetResult();

        OnVotingEnded();
        OnCompleted();
    }

    protected virtual void OnVotingStarted() { }
    protected virtual void OnVotingEnded() { }

    /// <summary>
    /// Do not call directly.
    /// </summary>
    protected virtual void OnCompleted() { }

    private VoteResult GetResult() => new(VotedYes, VotedNo);
}