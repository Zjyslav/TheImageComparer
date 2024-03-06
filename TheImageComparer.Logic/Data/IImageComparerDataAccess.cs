using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Data;

public interface IImageComparerDataAccess
{
    List<ImageModel> AddImages(IEnumerable<string> filePaths);
    void CreateVote(int votedForImageId, int votedAgainstImageId);
    List<ImageModel> GetAllImages();
    ImageModel? GetImageById(int id);
    List<VoteModel> GetVotesByImageId(int id);
    bool ImageAlreadyAdded(string filePath);
}