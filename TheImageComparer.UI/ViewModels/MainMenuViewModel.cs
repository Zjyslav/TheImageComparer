using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Services;

namespace TheImageComparer.UI.ViewModels;
public partial class MainMenuViewModel : ObservableObject
{
    private readonly IIOService _ioService;
    private readonly ISetSqliteDbFilePath _dbFilePath;

    public MainMenuViewModel(IIOService ioService, ISqliteDbFilePathService dbFilePath, IViewManagerService viewManager)
    {
        _ioService = ioService;
        _dbFilePath = dbFilePath;
        ViewManager = viewManager;
    }

    public IViewManagerService ViewManager { get; }

    [RelayCommand]
    private void OpenDatabase()
    {
        string? dbFilePath = _ioService.GetFilePathWithDialog("Plik .db|*.db");
        if (string.IsNullOrWhiteSpace(dbFilePath) == false)
        {
            _dbFilePath.DbFilePath = dbFilePath;
            // navigate to another view
        }
    }
}
