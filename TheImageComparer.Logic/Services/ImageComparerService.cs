using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TheImageComparer.Logic.Data;
using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Services;
public class ImageComparerService : IImageComparerService
{
    private readonly IImageComparerDataAccess _dataAccess;

    public ImageComparerService(IImageComparerDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<List<ImageModel>> GetAllImages()
    {
        return await _dataAccess.GetAllImages().ToListAsync();
    }

    public bool ImageAlreadyAdded(string filePath)
    {
        return _dataAccess.ImageAlreadyAdded(filePath).Result;
    }

    public async Task<IEnumerable<ImageModel>> AddImages(IEnumerable<string> filePaths)
    {
        return await _dataAccess.AddImages(filePaths);
    }

    public async Task<IEnumerable<VoteModel>> GetVotesByImageId(int id)
    {
        return await _dataAccess.GetVotesByImageId(id);
    }

    public async Task<int> GetScoreByImageId(int id)
    {
        var votes = (await GetVotesByImageId(id)).ToList();

        if (votes.Count == 0)
            return 0;

        int votesForCount = votes.Where(v => v.VotedFor.Id == id).Count();
        int votesAgainstCount = votes.Where(v => v.VotedAgainst.Id == id).Count();

        int score = (votesForCount * 1000) / (votesForCount + votesAgainstCount);

        return score;
    }

    public async Task<ImageModel?> GetImageToVote(VoteMode voteMode, ImageModel? anotherImage = null)
    {
        IEnumerable<ImageModel> images = await GetAllImages();

        if (anotherImage is not null)
        {
            List<int> idsToExclude = [anotherImage.Id];

            idsToExclude.AddRange(anotherImage.VotesFor
                .Select(v => v.VotedFor.Id));
            idsToExclude.AddRange(anotherImage.VotesAgainst
                .Select(v => v.VotedFor.Id));

            if (idsToExclude.Count < images.Count())
                images = images
                    .Where(i => idsToExclude.Contains(i.Id) == false);
        }


        if ((await GetAllImages()).Any() == false)
            return null;

        if (voteMode == VoteMode.LeastVotesLowestScoreFirst
            || voteMode == VoteMode.LeastVotesHighestScoreFirst
            || voteMode == VoteMode.LeastVotesFirst)
            images = images
                .GroupBy(i => i.VotesFor.Count + i.VotesAgainst.Count)
                .OrderBy(g => g.Key)
                .First();

        if (voteMode == VoteMode.LowestScoreFirst
            || voteMode == VoteMode.LeastVotesLowestScoreFirst)
            images = images
                .GroupBy(async i => await GetScoreByImageId(i.Id))
                .OrderBy(g => g.Key)
                .First();
        else if (voteMode == VoteMode.HighestScoreFirst
            || voteMode == VoteMode.LeastVotesHighestScoreFirst)
            images = images
                .GroupBy(async i => await GetScoreByImageId(i.Id))
                .OrderByDescending(g => g.Key)
                .First();

        return images.ToList()[Random.Shared.Next(0, images.Count())];
    }

    public async Task Vote(int votedForImageId, int votedAgainstImageId)
    {
        await _dataAccess.CreateVote(votedForImageId, votedAgainstImageId);
    }
}

public enum VoteMode
{
    [Display(Name = "Least Votes, Lowest Score First")]
    LeastVotesLowestScoreFirst,
    [Display(Name = "Least Votes, Highest Score First")]
    LeastVotesHighestScoreFirst,
    [Display(Name = "Least Votes First")]
    LeastVotesFirst,
    [Display(Name = "Lowest Score First")]
    LowestScoreFirst,
    [Display(Name = "Highest Score First")]
    HighestScoreFirst,
    [Display(Name = "Random")]
    Random
}