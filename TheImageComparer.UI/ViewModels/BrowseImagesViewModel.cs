using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class BrowseImagesViewModel : ObservableObject
{
    private readonly IImageComparerService _comparerService;
    private readonly IViewManagerService _viewManager;
    [ObservableProperty]
    private List<ImageModel> _images;

    public BrowseImagesViewModel(IImageComparerService comparerService, IViewManagerService viewManager)
    {
        _comparerService = comparerService;
        _viewManager = viewManager;
        Images = _comparerService.GetAllImages();
    }
    [RelayCommand]
    private void GoBack()
    {
        _viewManager.CloseView();
    }
}
