using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;

public partial class VoteStatsViewModel : ObservableObject
{
    [ObservableProperty]
    private Dictionary<int, int> _imagesCountByNumberOfVotes = [];

    [ObservableProperty]
    private Dictionary<int, int> _imagesCountByScore = [];
    private readonly IViewManagerService _viewManager;
    private readonly IStatsService _statsService;

    public VoteStatsViewModel(IViewManagerService viewManager,
                              IStatsService statsService)
    {
        _viewManager = viewManager;
        _statsService = statsService;

        var stats = _statsService.GetVoteStats().Result;

        ImagesCountByNumberOfVotes = stats.ImagesCountByNumberOfVotes;
        ImagesCountByScore = stats.ImagesCountByScore;
    }

    [RelayCommand]
    private void GoBack()
    {
        _viewManager.CloseView();
    }
}
