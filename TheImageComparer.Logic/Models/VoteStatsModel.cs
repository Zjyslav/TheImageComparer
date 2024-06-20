namespace TheImageComparer.Logic.Models;
public class VoteStatsModel
{
    public Dictionary<int, int> ImagesCountByNumberOfVotes { get; set; } = [];
    public Dictionary<int, int> ImagesCountByScore { get; set; } = [];
}
