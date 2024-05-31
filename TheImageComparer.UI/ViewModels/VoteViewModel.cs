using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Helpers;
using TheImageComparer.UI.Models;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;

public partial class VoteViewModel : ObservableObject
{
    private readonly IImageComparerService _comparerService;
    private readonly IViewManagerService _viewManager;

    [ObservableProperty]
    private ImageUIModel? _imageRight;
    [ObservableProperty]
    private ImageUIModel? _imageLeft;
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
    private async Task GetNewImages()
    {
        var image1 = await _comparerService.GetImageToVote(VoteMode);
        if (image1 is null)
            return;

        var image2 = await _comparerService.GetImageToVote(VoteMode, image1);
        if (image2 is null)
            return;

        ImageLeft = image1.ToUiModel();
        ImageRight = image2.ToUiModel();
    }

    [RelayCommand]
    private async Task Vote(ImageModel image)
    {
        if (ImageRight is null || ImageLeft is null)
            return;
        if (image == ImageRight)
            await _comparerService.Vote(ImageRight.Id, ImageLeft.Id);
        else if (image == ImageLeft)
            await _comparerService.Vote(ImageLeft.Id, ImageRight.Id);

        await GetNewImages();
    }

    [RelayCommand]
    private void GoBack()
    {
        _viewManager.CloseView();
    }
}
