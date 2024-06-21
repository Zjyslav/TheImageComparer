using System.Windows.Controls;
using TheImageComparer.UI.ViewModels;

namespace TheImageComparer.UI.Views;

public partial class ImageDetailsView : UserControl, IView
{
    private readonly ImageDetailsViewModel _viewModel;

    public ImageDetailsView(ImageDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }
}
