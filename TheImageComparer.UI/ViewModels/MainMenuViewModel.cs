using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.IO;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Messages;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class MainMenuViewModel : ObservableObject
{
    private readonly IIOService _ioService;
    private readonly IResourcesService _resources;

    public MainMenuViewModel(IIOService ioService,
                             IResourcesService resources)
    {
        _ioService = ioService;
        _resources = resources;
    }

    [RelayCommand]
    private void OpenDatabase()
    {
        string? dbFilePath = _ioService.GetFilePathWithDialog("Plik .db|*.db");
        if (string.IsNullOrWhiteSpace(dbFilePath) == false)
        {
            WeakReferenceMessenger.Default.Send<OpenDbMessage>(new(dbFilePath));
        }
    }
    [RelayCommand]
    private void CreateDatabase()
    {
        string? dbSavePath = _ioService.GetSaveFilePathWithDialog("Plik .db|*.db", "db", "images");
        if (string.IsNullOrWhiteSpace(dbSavePath) == false)
        {
            File.WriteAllBytes(dbSavePath, _resources.DatabaseTemplate);
            WeakReferenceMessenger.Default.Send<OpenDbMessage>(new(dbSavePath));
        }
    }
}
