using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Models;

namespace TheImageComparer.UI.Helpers;
public static class ImageComparerServiceExtensions
{
    public static ImageUIModel ConvertImageModelToUIModel(this IImageComparerService service, ImageModel image)
    {
        ImageUIModel output = new()
        {
            Id = image.Id,
            FilePath = image.FilePath,
            IsArchived = image.IsArchived,
            PossibleDuplicate = image.PossibleDuplicate,
            Votes = service.GetVotesByImageId(image.Id),
            Score = service.GetScoreByImageId(image.Id)
        };
        return output;
    }
}
