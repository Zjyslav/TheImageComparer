using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Services;

public interface IImageComparerService
{
    IEnumerable<ImageModel> AddImages(IEnumerable<string> filePaths);
    IEnumerable<ImageModel> GetAllImages();
    ImageModel? GetImageToVote(VoteMode voteMode, ImageModel? anotherImage = null);
    int GetScoreByImageId(int id);
    IEnumerable<VoteModel> GetVotesByImageId(int id);
    bool ImageAlreadyAdded(string filePath);
    void Vote(int votedForImageId, int votedAgainstImageId);
}