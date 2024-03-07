using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Data;

public interface IImageComparerDataAccess
{
    IEnumerable<ImageModel> AddImages(IEnumerable<string> filePaths);
    void CreateVote(int votedForImageId, int votedAgainstImageId);
    IEnumerable<ImageModel> GetAllImages();
    ImageModel? GetImageById(int id);
    IEnumerable<VoteModel> GetVotesByImageId(int id);
    bool ImageAlreadyAdded(string filePath);
}