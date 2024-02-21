using System.Windows.Controls;
using TheImageComparer.UI.ViewModels;

namespace TheImageComparer.UI.Views;
public partial class OpenFolderView : UserControl, IView
{
    private readonly OpenFolderViewModel _viewModel;

    public OpenFolderView(OpenFolderViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }
}
