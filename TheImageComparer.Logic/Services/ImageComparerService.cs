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
}
