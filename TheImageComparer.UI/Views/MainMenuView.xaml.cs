using System.Windows.Controls;
using TheImageComparer.UI.ViewModels;

namespace TheImageComparer.UI.Views;
public partial class MainMenuView : UserControl, IView
{
    private readonly MainMenuViewModel _viewModel;

    public MainMenuView(MainMenuViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }
}
