using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Data;

public interface IImageComparerDataAccess
{
    List<ImageModel> GetAllImages();
}