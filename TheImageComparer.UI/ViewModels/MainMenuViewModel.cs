using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class MainMenuViewModel : ObservableObject
{
    private readonly IIOService _ioService;
    private readonly ISetSqliteDbFilePath _dbFilePath;
    private readonly IViewManagerService _viewManager;
    private readonly IResourcesService _resources;

    public MainMenuViewModel(IIOService ioService,
                             ISqliteDbFilePathService dbFilePath,
                             IViewManagerService viewManager,
                             IResourcesService resources)
    {
        _ioService = ioService;
        _dbFilePath = dbFilePath;
        _viewManager = viewManager;
        _resources = resources;
    }

    [RelayCommand]
    private void OpenDatabase()
    {
        string? dbFilePath = _ioService.GetFilePathWithDialog("Plik .db|*.db");
        if (string.IsNullOrWhiteSpace(dbFilePath) == false)
        {
            _dbFilePath.DbFilePath = dbFilePath;
            _viewManager.OpenView(ViewName.DatabaseMenu);
        }
    }
    [RelayCommand]
    private void CreateDatabase()
    {
        string? dbSavePath = _ioService.GetSaveFilePathWithDialog("Plik .db|*.db", "db", "images");
        if (string.IsNullOrWhiteSpace(dbSavePath) == false)
        {
            File.WriteAllBytes(dbSavePath, _resources.DatabaseTemplate);
            _dbFilePath.DbFilePath = dbSavePath;
            _viewManager.OpenView(ViewName.DatabaseMenu);
        }
    }
}
