using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Models;

namespace TheImageComparer.UI.Helpers;
public static class ImageComparerServiceExtensions
{
    public async static Task<ImageUIModel> ConvertImageModelToUIModel(this IImageComparerService service, ImageModel image)
    {
        ImageUIModel output = new()
        {
            Id = image.Id,
            FilePath = image.FilePath,
            IsArchived = image.IsArchived,
            PossibleDuplicate = image.PossibleDuplicate,
            Votes = (await service.GetVotesByImageId(image.Id)).ToList(),
            Score = await service.GetScoreByImageId(image.Id)
        };
        return output;
    }
}
