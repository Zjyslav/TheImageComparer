using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Data;

public interface IImageComparerDataAccess
{
    IEnumerable<ImageModel> AddImages(IEnumerable<string> filePaths);
    void CreateVote(int votedForImageId, int votedAgainstImageId);
    List<ImageModel> GetAllImages();
    List<VoteModel> GetAllVotes();
    ImageModel? GetImageById(int id);
    IEnumerable<VoteModel> GetVotesByImageId(int id);
    bool ImageAlreadyAdded(string filePath);
}