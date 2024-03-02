using System.Reflection.Metadata.Ecma335;
using System.Windows;
using System.Windows.Controls;
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

        _viewModel.StartViewer(shellViewer);
    }
}
