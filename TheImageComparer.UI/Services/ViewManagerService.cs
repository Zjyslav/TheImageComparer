using System.Diagnostics;
using System.Windows.Controls;
using TheImageComparer.UI.Helpers.StartupHelpers;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI.Services;
public class ViewManagerService : IViewManagerService
{
    private ContentControl? _viewer;
    private readonly IAbstractFactory<DatabaseMenuView> _databaseMenuFactory;
    private readonly IAbstractFactory<OpenFolderView> _openFolderFactory;
    private readonly IAbstractFactory<BrowseImagesView> _browseImagesFactory;
    private readonly IAbstractFactory<VoteView> _voteFactory;
    private Stack<IView> _views = new();
    private ViewName _defaultViewName = ViewName.DatabaseMenu;
    private bool _running = true;

    public ViewManagerService(IAbstractFactory<DatabaseMenuView> databaseMenuFactory,
                              IAbstractFactory<OpenFolderView> openFolderFactory,
                              IAbstractFactory<BrowseImagesView> browseImagesFactory,
                              IAbstractFactory<VoteView> voteFactory)
    {
        _databaseMenuFactory = databaseMenuFactory;
        _openFolderFactory = openFolderFactory;
        _browseImagesFactory = browseImagesFactory;
        _voteFactory = voteFactory;
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

    public async Task StartAsync(ContentControl viewer)
    {
        _viewer = viewer;
        AddDefaultView();
        ShowInShellViewer();
        while (_running)
        {
            await Task.Delay(100);
        }
    }

    private void ShowInShellViewer()
    {
        if (_views.Any() == false)
        {
            _running = false;
            return;
        }

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
            case ViewName.DatabaseMenu:
                return _databaseMenuFactory.Create();
            case ViewName.OpenFolder:
                return _openFolderFactory.Create();
            case ViewName.BrowseImages:
                return _browseImagesFactory.Create();
            case ViewName.Vote:
                return _voteFactory.Create();
            default:
                return null;
        }
    }
}

public enum ViewName
{
    MainMenu,
    DatabaseMenu,
    OpenFolder,
    BrowseImages,
    Vote
}