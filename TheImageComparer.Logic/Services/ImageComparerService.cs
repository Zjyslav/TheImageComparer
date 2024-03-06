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

    public List<ImageModel> GetAllImages()
    {
        return _dataAccess.GetAllImages();
    }

    public bool ImageAlreadyAdded(string filePath)
    {
        return _dataAccess.ImageAlreadyAdded(filePath);
    }

    public List<ImageModel> AddImages(IEnumerable<string> filePaths)
    {
        return _dataAccess.AddImages(filePaths);
    }

    public List<VoteModel> GetVotesByImageId(int id)
    {
        return _dataAccess.GetVotesByImageId(id);
    }

    public int GetScoreByImageId(int id)
    {
        var votes = GetVotesByImageId(id);

        if(votes.Any() == false)
            return 0;

        int votesForCount = votes.Where(v => v.VotedFor.Id == id).Count();
        int votesAgainstCount = votes.Where(v => v.VotedAgainst.Id == id).Count();

        int score = (votesForCount * 1000) / (votesForCount + votesAgainstCount);

        return score;
    }

    public ImageModel? GetImageToVote(VoteMode voteMode, ImageModel? anotherImage = null)
    {
        IEnumerable<ImageModel> images = GetAllImages();

        if (anotherImage is not null)
        {
            List<int> idsToExclude = [anotherImage.Id];
            idsToExclude.AddRange(GetVotesByImageId(anotherImage.Id)
                .Select(v =>
                    v.VotedFor.Id == anotherImage.Id
                    ? v.VotedAgainst.Id
                    : v.VotedFor.Id));

            if (idsToExclude.Count < images.Count())
                images = images
                    .Where(i => idsToExclude.Contains(i.Id) == false);
        }


        if (GetAllImages().Any() == false)
            return null;

        if (voteMode == VoteMode.LeastVotesLowestScoreFirst || voteMode == VoteMode.LeastVotesHighestScoreFirst)
            images = images
                .GroupBy(i => GetVotesByImageId(i.Id).Count)
                .OrderBy(g => g.Key)
                .First();

        if (voteMode == VoteMode.LowestScoreFirst || voteMode == VoteMode.LeastVotesLowestScoreFirst)
            images = images
                .GroupBy(i => GetScoreByImageId(i.Id))
                .OrderBy(g => g.Key)
                .First();
        else if (voteMode == VoteMode.HighestScoreFirst || voteMode == VoteMode.LeastVotesHighestScoreFirst)
            images = images
                .GroupBy(i => GetScoreByImageId(i.Id))
                .OrderByDescending(g => g.Key)
                .First();

        return images.ToList()[Random.Shared.Next(0, images.Count())];
    }

    public void Vote(int votedForImageId, int votedAgainstImageId)
    {
        _dataAccess.CreateVote(votedForImageId, votedAgainstImageId);
    }
}

public enum VoteMode
{
    [Display(Name = "Least Votes, Lowest Score First")]
    LeastVotesLowestScoreFirst,
    [Display(Name = "Least Votes, Highest Score First")]
    LeastVotesHighestScoreFirst,
    [Display(Name = "Lowest Score First")]
    LowestScoreFirst,
    [Display(Name = "Highest Score First")]
    HighestScoreFirst,
    [Display(Name = "Random")]
    Random
}