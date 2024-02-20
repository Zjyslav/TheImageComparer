using TheImageComparer.Logic.Data;
using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Services;
public class ImageComparerService : IImageComparerService
{
    private readonly IImageComparerDataAccess _dataAccess;

    public ImageComparerService(IImageComparerDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public List<ImageModel> GetAllImages()
    {
        return _dataAccess.GetAllImages();
    }
}
