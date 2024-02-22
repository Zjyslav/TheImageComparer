using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Services;

public interface IImageComparerService
{
    List<ImageModel> AddImages(IEnumerable<string> filePaths);
    List<ImageModel> GetAllImages();
    bool ImageAlreadyAdded(string filePath);
}