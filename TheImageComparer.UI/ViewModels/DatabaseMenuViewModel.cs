using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheImageComparer.Logic.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class DatabaseMenuViewModel : ObservableObject
{
    private readonly IImageComparerService _comparerService;

    public DatabaseMenuViewModel(IImageComparerService comparerService)
    {
        _comparerService = comparerService;
    }
}
