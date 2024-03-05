using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Data;

public interface IImageComparerDataAccess
{
    List<ImageModel> AddImages(IEnumerable<string> filePaths);
    void CreateVote(ImageModel votedFor, ImageModel votedAgainst);
    List<ImageModel> GetAllImages();
    List<VoteModel> GetVotesByImageId(int id);
    bool ImageAlreadyAdded(string filePath);
}