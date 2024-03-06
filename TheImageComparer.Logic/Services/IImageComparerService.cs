using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Services;

public interface IImageComparerService
{
    List<ImageModel> AddImages(IEnumerable<string> filePaths);
    List<ImageModel> GetAllImages();
    ImageModel? GetImageToVote(VoteMode voteMode, ImageModel? anotherImage = null);
    int GetScoreByImageId(int id);
    List<VoteModel> GetVotesByImageId(int id);
    bool ImageAlreadyAdded(string filePath);
    void Vote(int votedForImageId, int votedAgainstImageId);
}