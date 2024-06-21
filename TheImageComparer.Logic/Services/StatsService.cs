using Microsoft.EntityFrameworkCore;
using TheImageComparer.Logic.Data;
using TheImageComparer.Logic.Helpers;
using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Services;
public class StatsService : IStatsService
{
    private readonly IImageComparerDataAccess _dataAccess;

    public StatsService(IImageComparerDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<VoteStatsModel> GetVoteStats()
    {
        var imagesCountByNumberOfVotes = await GetImagesCountByNumberOfVotes();
        var imagesCountByScore = await GetImagesCountByScore();

        VoteStatsModel output = new()
        {
            ImagesCountByNumberOfVotes = imagesCountByNumberOfVotes,
            ImagesCountByScore = imagesCountByScore
        };

        return output;
    }

    private async Task<Dictionary<int, int>> GetImagesCountByNumberOfVotes()
    {
        var output = await _dataAccess.GetAllImages()
            .GroupBy(i => i.VotesFor.Count + i.VotesAgainst.Count)
            .ToDictionaryAsync(g => g.Key, g => g.Count());

        return SortDictionary(output);
    }

    private async Task<Dictionary<int, int>> GetImagesCountByScore()
    {
        var output = (await _dataAccess.GetAllImages().ToListAsync())
            .GroupBy(i => i.GetScore())
            .ToDictionary(g => g.Key, g => g.Count());

        return SortDictionary(output);
    }

    private Dictionary<int, int> SortDictionary(Dictionary<int, int> dictionary)
    {
        return dictionary.OrderBy(x => x.Key).ToDictionary();
    }
}
