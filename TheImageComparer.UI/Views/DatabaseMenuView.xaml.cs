using System.Windows.Controls;
using TheImageComparer.UI.ViewModels;

namespace TheImageComparer.UI.Views;
public partial class DatabaseMenuView : UserControl, IView
{
    private readonly DatabaseMenuViewModel _viewModel;

    public DatabaseMenuView(DatabaseMenuViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }
}
