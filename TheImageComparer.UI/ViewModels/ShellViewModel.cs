using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows.Controls;
using TheImageComparer.Logic.Services;
using TheImageComparer.UI.Helpers.StartupHelpers;
using TheImageComparer.UI.Messages;
using TheImageComparer.UI.Services;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI.ViewModels;
public partial class ShellViewModel : ObservableObject
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IView _mainMenu;
    private ContentControl? _viewer;
    public ShellViewModel(IServiceScopeFactory scopeFactory, IAbstractFactory<MainMenuView> mainMenuFactory)
    {
        _scopeFactory = scopeFactory;
        _mainMenu = mainMenuFactory.Create();
        RegisterMessages();
    }

    public void StartViewer(ContentControl viewer)
    {
        _viewer = viewer;
        _viewer.Content = _mainMenu;
    }

    private void RegisterMessages()
    {
        WeakReferenceMessenger.Default.Register<OpenDbMessage>(this, async (r, m) =>
        {
            using var scope = _scopeFactory.CreateScope();
            scope.ServiceProvider.GetRequiredService<ISetSqliteDbFilePath>().DbFilePath = m.DbFilePath;

            IViewManagerService viewManager = scope.ServiceProvider.GetRequiredService<IViewManagerService>();
            if (_viewer is not null)
            {
                await viewManager.StartAsync(_viewer);
                _viewer.Content = _mainMenu;
            }
        }); 
    }
}
