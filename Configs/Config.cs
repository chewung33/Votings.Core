using Voting.Core.Configs;
using Voting.Core.Translations;

namespace Voting.Core;

public sealed class Config
{
    public VotingFFConfig VotingFF { get; set; } = new();
    public Translation Translation { get; set; } = new();
}
