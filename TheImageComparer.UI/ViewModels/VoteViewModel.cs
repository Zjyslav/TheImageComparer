using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Models;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;

public partial class VoteViewModel : ObservableObject
{
    private readonly IImageComparerService _comparerService;
    private readonly IViewManagerService _viewManager;

    [ObservableProperty]
    private ImageModel? _imageRight;
    [ObservableProperty]
    private ImageModel? _imageLeft;
    [ObservableProperty]
    private VoteMode voteMode = VoteMode.LeastVotesLowestScoreFirst;
    public IEnumerable<VoteMode> VoteModeValues
    {
        get
        {
            return Enum.GetValues(typeof(VoteMode)).Cast<VoteMode>();
        }
    }

    public VoteViewModel(IImageComparerService comparerService, IViewManagerService viewManager)
    {
        _comparerService = comparerService;
        _viewManager = viewManager;

        GetNewImages();
    }

    [RelayCommand]
    private void GetNewImages()
    {
        var image1 = _comparerService.GetImageToVote(VoteMode);
        if (image1 is null)
            return;

        var image2 = _comparerService.GetImageToVote(VoteMode, image1);
        if (image2 is null)
            return;

        ImageLeft = image1;
        ImageRight = image2;
    }

    [RelayCommand]
    private void Vote(ImageModel image)
    {
        GetNewImages();
    }

    [RelayCommand]
    private void GoBack()
    {
        _viewManager.CloseView();
    }
}
