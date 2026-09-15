# What is this?

### This is a framework for creating and managing custom votes. You can create a vote by:

1. Adding code to the current plugin.
2. Inheriting the `VoteBase` or `VoteAction` class in your plugin and registering it using `VoteManager.AddVote(VoteBase base);`
3. Starting a vote whenever needed using `VoteManager.StartVote(int index)`.

#### You can find an example in `Votings/VotingFriendlyFire`.

##### The framework is still under development and currently lacks some features for convenient use.
##### A basic vote to enable Friendly Fire at the beginning of a round has been added. To disable the Friendly Fire vote, change the configuration setting `Config - VotingFF - IsEnabled - false`.