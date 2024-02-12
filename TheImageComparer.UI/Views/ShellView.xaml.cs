using System.Windows;
using TheImageComparer.UI.ViewModels;

namespace TheImageComparer.UI.Views;
public partial class ShellView : Window
{
    private readonly ShellViewModel _viewModel;

    public ShellView(ShellViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }
}
