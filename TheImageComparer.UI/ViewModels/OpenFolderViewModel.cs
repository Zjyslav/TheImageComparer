using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.IO;
using System.Windows;
using TheImageComparer.Logic.Services;
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
    private readonly IImageComparerService _comparerService;
    [ObservableProperty]
    private bool _allSelected = true;

    public OpenFolderViewModel(IViewManagerService viewManager, IImageComparerService comparerService)
    {
        RegisterMessages();
        _viewManager = viewManager;
        _comparerService = comparerService;
    }

    [RelayCommand]
    private void GoBack()
    {
        _viewManager.CloseView();
    }

    [RelayCommand]
    private void SelectAll()
    {
        foreach (var imageFilePath in ImageFiles)
        {
            imageFilePath.Selected = true;
        }
        AllSelected = true;
    }

    [RelayCommand]
    private void DeselectAll()
    {
        foreach (var imageFilePath in ImageFiles)
        {
            imageFilePath.Selected = false;
        }
        AllSelected = false;
    }

    [RelayCommand]
    private void AddSelectedImages()
    {
        var selected = ImageFiles
            .Where(i => i.Selected)
            .Select(i => i.FilePath)
            .ToList();

        _comparerService.AddImages(selected);

        _viewManager.CloseView();
    }

    private void RegisterMessages()
    {
        WeakReferenceMessenger.Default.Register<OpenFolderMessage>(this, (r, m) =>
        {
            OpenFolder(m.FolderPath);
            WeakReferenceMessenger.Default.Unregister<OpenFolderMessage>(r);
        });
    }

    private void OpenFolder(string folderPath)
    {
        FolderPath = folderPath;

        string[] extensions = [".jpg", ".png", ".jpeg", ".tiff", ".bmp"];

        ImageFiles = Directory
            .EnumerateFiles(folderPath)
            .Where(f =>
                extensions.Contains(Path.GetExtension(f).ToLower())
                && _comparerService.ImageAlreadyAdded(f) == false)
            .Select(f => new ImageFilePathModel(f))
            .ToList();

        if (ImageFiles.Any())
            return;

        MessageBox.Show("No new images found.");
        _viewManager.CloseView();
    }
}
