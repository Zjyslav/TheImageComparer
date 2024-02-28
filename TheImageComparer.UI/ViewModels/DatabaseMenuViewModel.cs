using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Messages;
using TheImageComparer.UI.Services;
using Microsoft.Data.Sqlite;

namespace TheImageComparer.UI.ViewModels;
public partial class DatabaseMenuViewModel : ObservableObject
{
    private readonly IImageComparerService _comparerService;

    [ObservableProperty]
    private IGetSqliteDbFilePath _dbFilePath;
    private readonly IViewManagerService _viewManager;
    private readonly IIOService _ioService;
    private ISetSqliteDbFilePath _setDbFilePath;
    public DatabaseMenuViewModel(IImageComparerService comparerService,
                                 ISqliteDbFilePathService dbFilePath,
                                 IViewManagerService viewManager,
                                 IIOService ioService)
    {
        _comparerService = comparerService;
        _dbFilePath = dbFilePath;
        _setDbFilePath = dbFilePath;
        _viewManager = viewManager;
        _ioService = ioService;
    }

    [RelayCommand]
    private void CloseDatabase()
    {
        _setDbFilePath.DbFilePath = null;
        _viewManager.CloseView();
        SqliteConnection.ClearAllPools();
    }

    [RelayCommand]
    private void OpenFolder()
    {
        string? folderPath = _ioService.GetFolderPathWithDialog();
        if (folderPath is null)
            return;

        _viewManager.OpenView(ViewName.OpenFolder);
        WeakReferenceMessenger.Default.Send(new OpenFolderMessage(folderPath));
    }
    [RelayCommand]
    private void BrowseImages()
    {
        _viewManager.OpenView(ViewName.BrowseImages);
    }

    [RelayCommand]
    private void Vote()
    {
        _viewManager.OpenView(ViewName.Vote);
    }
}
