using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TheImageComparer.UI.Messages;
using TheImageComparer.UI.Models;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class OpenFolderViewModel : ObservableObject
{
    [ObservableProperty]
    private string _folderPath = "";
    [ObservableProperty]
    List<ImageFilePathModel> _imageFiles = new();
    private readonly IViewManagerService _viewManager;

    public OpenFolderViewModel(IViewManagerService viewManager)
    {
        RegisterMessages();
        _viewManager = viewManager;
    }

    [RelayCommand]
    private void GoBack()
    {
        _viewManager.CloseView();
    }

    private void RegisterMessages()
    {
        WeakReferenceMessenger.Default.Register<OpenFolderMessage>(this, (r, m) =>
        {
            OpenFolder(m.FolderPath);
        });
    }

    private void OpenFolder(string folderPath)
    {
        FolderPath = folderPath;

        string[] extensions = [".jpg", ".png", ".jpeg", ".tiff", ".bmp"];

        ImageFiles = Directory
            .EnumerateFiles(folderPath)
            .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()))
            .Select(f => new ImageFilePathModel(f))
            .ToList();
    }
}
