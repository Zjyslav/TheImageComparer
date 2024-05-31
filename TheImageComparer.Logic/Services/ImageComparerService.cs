using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TheImageComparer.Logic.Data;
using TheImageComparer.Logic.Helpers;
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

    public async Task Vote(int votedForImageId, int votedAgainstImageId)
    {
        await _dataAccess.CreateVote(votedForImageId, votedAgainstImageId);
    }

    public async Task<ImageModel?> GetImageToVote(VoteMode voteMode, ImageModel? anotherImage = null)
    {
        List<ImageModel> allImages = await GetAllImages();

        if (allImages.Count == 0)
        {
            return null;
        }

        List<ImageModel> availableImages = GetAvailableImages(allImages, voteMode, anotherImage);
        int imageIndex = Random.Shared.Next(0, availableImages.Count);
        var image = availableImages.ElementAt(imageIndex);
        return image;
    }

    private static List<ImageModel> GetAvailableImages(List<ImageModel> allImages, VoteMode voteMode, ImageModel? anotherImage)
    {
        IEnumerable<ImageModel> filteredImages = allImages;

        if (anotherImage is not null)
        {
            filteredImages = ExcludeAnotherImageAndAlreadyVoted(anotherImage, filteredImages);
        }

        if (LeastVotesFirst(voteMode))
        {
            filteredImages = FilterLowestVotes(filteredImages);
        }

        filteredImages = FilterByScore(voteMode, filteredImages);

        var availableImages = filteredImages.ToList();
        return availableImages;
    }

    private static IEnumerable<ImageModel> ExcludeAnotherImageAndAlreadyVoted(ImageModel? anotherImage, IEnumerable<ImageModel> filteredImages)
    {
        List<int> idsToExclude = [anotherImage.Id];

        idsToExclude.AddRange(anotherImage.VotesFor
            .Select(v => v.VotedFor.Id));
        idsToExclude.AddRange(anotherImage.VotesAgainst
            .Select(v => v.VotedFor.Id));

        if (idsToExclude.Count < filteredImages.Count())
            filteredImages = filteredImages
                .Where(i => idsToExclude.Contains(i.Id) == false);
        return filteredImages;
    }

    private static IEnumerable<ImageModel> FilterLowestVotes(IEnumerable<ImageModel> filteredImages)
    {
        var groupedByCount = filteredImages
                        .GroupBy(i => i.GetVotesCount());

        var ordered = groupedByCount
            .OrderBy(g => g.Key);

        filteredImages = ordered
            .First(g => g.Any());
        return filteredImages;
    }

    private static IEnumerable<ImageModel> FilterByScore(VoteMode voteMode, IEnumerable<ImageModel> filteredImages)
    {
        var groupedByScore = filteredImages
                        .GroupBy(i => i.GetScore())
                        .ToList();

        if (LowestScoreFirst(voteMode))
        {
            filteredImages = FilterLowestScore(groupedByScore);
        }
        else if (HighestScoreFirst(voteMode))
        {
            filteredImages = FilterHighestScore(groupedByScore);
        }

        return filteredImages;
    }

    private static IEnumerable<ImageModel> FilterLowestScore(List<IGrouping<int, ImageModel>> groupedByScore)
    {
        return groupedByScore
                        .OrderBy(g => g.Key)
                        .First(g => g.Any());
    }

    private static IEnumerable<ImageModel> FilterHighestScore(List<IGrouping<int, ImageModel>> groupedByScore)
    {
        return groupedByScore
                        .OrderByDescending(g => g.Key)
                        .First(g => g.Any());
    }

    private static bool LeastVotesFirst(VoteMode voteMode)
    {
        return voteMode == VoteMode.LeastVotesLowestScoreFirst
                    || voteMode == VoteMode.LeastVotesHighestScoreFirst
                    || voteMode == VoteMode.LeastVotesFirst;
    }
    private static bool LowestScoreFirst(VoteMode voteMode)
    {
        return voteMode == VoteMode.LowestScoreFirst
                    || voteMode == VoteMode.LeastVotesLowestScoreFirst;
    }

    private static bool HighestScoreFirst(VoteMode voteMode)
    {
        return voteMode == VoteMode.HighestScoreFirst
                    || voteMode == VoteMode.LeastVotesHighestScoreFirst;
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