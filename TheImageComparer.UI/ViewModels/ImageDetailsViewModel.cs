using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TheImageComparer.Logic.Models;
using TheImageComparer.UI.Messages;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class ImageDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private ImageModel? _image;
    private readonly IViewManagerService _viewManager;

    public ImageDetailsViewModel(IViewManagerService viewManager)
    {
        RegisterMessages();
        _viewManager = viewManager;
    }

    private void RegisterMessages()
    {
        WeakReferenceMessenger.Default.Register<ShowImageDetailsMessage>(this, (r, m) =>
        {
            Image = m.Image;
        });
    }

    [RelayCommand]
    private void GoBack()
    {
        _viewManager.CloseView();
    }
}
