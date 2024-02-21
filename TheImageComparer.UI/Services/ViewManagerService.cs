using System.Windows.Controls;
using TheImageComparer.UI.StartupHelpers;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI.Services;
public class ViewManagerService : IViewManagerService
{
    private ContentControl? _viewer;
    private readonly IAbstractFactory<MainMenuView> _mainMenuFactory;
    private readonly IAbstractFactory<DatabaseMenuView> _databaseMenuFactory;
    private readonly IAbstractFactory<OpenFolderView> _openFolderFactory;
    private Stack<IView> _views = new();
    private ViewName _defaultViewName = ViewName.MainMenu;

    public ViewManagerService(IAbstractFactory<MainMenuView> mainMenuFactory,
                              IAbstractFactory<DatabaseMenuView> databaseMenuFactory,
                              IAbstractFactory<OpenFolderView> openFolderFactory)
    {
        _mainMenuFactory = mainMenuFactory;
        _databaseMenuFactory = databaseMenuFactory;
        _openFolderFactory = openFolderFactory;
    }

    public void OpenView(ViewName viewName)
    {
        IView? newView = CreateView(viewName);
        if (newView is null)
            return;

        _views.Push(newView);
        ShowInShellViewer();
    }

    public void CloseView()
    {
        _views.Pop();
        ShowInShellViewer();
    }

    public void Start(ContentControl viewer)
    {
        _viewer = viewer;
        _views.Clear();
        ShowInShellViewer();
    }

    private void ShowInShellViewer()
    {
        if (_views.Any() == false)
            AddDefaultView();

        if (_viewer is null)
            return;

        _viewer.Content = _views.Peek();
    }

    private void AddDefaultView()
    {
        IView? defaultView = CreateView(_defaultViewName);
        if (defaultView is null)
            return;

        _views.Push(defaultView);
    }

    private IView? CreateView(ViewName viewName)
    {
        switch (viewName)
        {
            case ViewName.MainMenu:
                return _mainMenuFactory.Create();
            case ViewName.DatabaseMenu:
                return _databaseMenuFactory.Create();
            case ViewName.OpenFolder:
                return _openFolderFactory.Create();
            default:
                return null;
        }
    }
}

public enum ViewName
{
    MainMenu,
    DatabaseMenu,
    OpenFolder
}