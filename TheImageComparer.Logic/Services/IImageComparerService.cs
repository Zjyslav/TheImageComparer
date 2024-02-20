using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Services;

public interface IImageComparerService
{
    List<ImageModel> GetAllImages();
}