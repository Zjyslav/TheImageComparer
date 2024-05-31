using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Services;
public interface IImageComparerService
{
    Task<IEnumerable<ImageModel>> AddImages(IEnumerable<string> filePaths);
    Task<List<ImageModel>> GetAllImages();
    Task<ImageModel?> GetImageToVote(VoteMode voteMode, ImageModel? anotherImage = null);
    bool ImageAlreadyAdded(string filePath);
    Task Vote(int votedForImageId, int votedAgainstImageId);
}