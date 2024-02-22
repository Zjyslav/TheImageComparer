using System.Windows.Controls;
using TheImageComparer.UI.ViewModels;

namespace TheImageComparer.UI.Views;
public partial class BrowseImagesView : UserControl, IView
{
    private readonly BrowseImagesViewModel _viewModel;

    public BrowseImagesView(BrowseImagesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }
}
