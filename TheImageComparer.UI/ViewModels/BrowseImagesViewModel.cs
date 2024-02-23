using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Models;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class BrowseImagesViewModel : ObservableObject
{
    private readonly IImageComparerService _comparerService;
    private readonly IViewManagerService _viewManager;
    [ObservableProperty]
    private List<ImageUIModel> _images;

    public BrowseImagesViewModel(IImageComparerService comparerService, IViewManagerService viewManager)
    {
        _comparerService = comparerService;
        _viewManager = viewManager;
        Images = _comparerService
            .GetAllImages()
            .Select(i => ConvertImageModelToUIModel(i))
            .OrderByDescending(i => i.Score)
            .ThenBy(i => i.FilePath)
            .ToList();
    }
    [RelayCommand]
    private void GoBack()
    {
        _viewManager.CloseView();
    }

    private ImageUIModel ConvertImageModelToUIModel(ImageModel image)
    {
        ImageUIModel output = new()
        {
            Id = image.Id,
            FilePath = image.FilePath,
            IsArchived = image.IsArchived,
            PossibleDuplicate = image.PossibleDuplicate,
            Votes = _comparerService.GetVotesByImageId(image.Id),
            Score = _comparerService.GetScoreByImageId(image.Id)
        };
        return output;
    }
}
