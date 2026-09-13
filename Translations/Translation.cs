namespace Voting.Core.Translations;

public sealed class Translation
{
    public string Successfully { get; set; } = "Successfully";
    public string Enabled { get; set; } = "Enabled";
    public string Disabled { get; set; } = "Disabled";

    public string VoteUsing { get; set; } = "Using: .vote <yes | no>";
    public string VoteError { get; set; } = "There are currently no polls available, or you have already voted.";

    public string VotingMessage { get; set; } = """
        <b><color=yellow>Voting: <color=blue>{0}</color>
        (Remained: {1} seconds)
        <color=green>Agree: {2} (.vote yes)</color> | <color=red>Decline: {3} (.vote no)</color></color></b>
        """;
    public string ResultOfTheVotingMessage { get; set; } = "<b><color=yellow>Resulf of the vote: {0}\n(Agree: {1} | Decline: {2})</color></b>";

    public VotingFF VotingFF { get; set; } = new();
}

//public sealed class Translation
//{
//    public string Successfully { get; set; } = "Успешно";
//    public string Enabled { get; set; } = "Включено";
//    public string Disabled { get; set; } = "Отключено";
//    public string VoteUsing { get; set; } = "Использование: .vote <yes | no>";
//    public string VoteError { get; set; } = "В данный момент не проводится голосований либо вы уже проголосовали.";

//    public string VotingMessage { get; set; } = """
//        <b><color=yellow>Голосование: <color=blue>{0}</color>
//        (Осталось: {1} секунд)
//        <color=green>За: {2} (.vote yes)</color> | <color=red>Против: {3} (.vote no)</color></color></b>
//        """;
//    public string ResultOfTheVotingMessage { get; set; } = "<b><color=yellow>Результат голосования: {0}\n(За: {1} | Против: {2})</color></b>";
//}