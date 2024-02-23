using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Data;

public interface IImageComparerDataAccess
{
    List<ImageModel> AddImages(IEnumerable<string> filePaths);
    List<ImageModel> GetAllImages();
    List<VoteModel> GetVotesByImageId(int id);
    bool ImageAlreadyAdded(string filePath);
}