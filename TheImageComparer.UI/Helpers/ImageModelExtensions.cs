using TheImageComparer.Logic.Helpers;
using TheImageComparer.Logic.Models;
using TheImageComparer.UI.Models;

namespace TheImageComparer.UI.Helpers;
public static class ImageModelExtensions
{
    public static ImageUIModel ToUiModel(this ImageModel image)
    {
        ImageUIModel output = new()
        {
            Id = image.Id,
            FilePath = image.FilePath,
            IsArchived = image.IsArchived,
            PossibleDuplicate = image.PossibleDuplicate,
            Votes = image.GetAllVotes(),
            Score = image.GetScore()
        };
        return output;
    }
}
