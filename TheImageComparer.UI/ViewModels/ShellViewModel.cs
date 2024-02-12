using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class ShellViewModel : ObservableObject
{
    private readonly IViewManagerService _viewManager;

    public ShellViewModel(IViewManagerService viewManager)
    {
        _viewManager = viewManager;
        
    }

    public void StartViewManager(ContentControl viewer)
    {
        _viewManager.Start(viewer);
    }
}
