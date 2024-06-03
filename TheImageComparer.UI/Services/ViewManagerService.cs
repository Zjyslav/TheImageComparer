using System.Diagnostics;
using System.Windows.Controls;
using TheImageComparer.UI.Factories;
using TheImageComparer.UI.Helpers.StartupHelpers;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI.Services;
public class ViewManagerService : IViewManagerService
{
    private ContentControl? _viewer;
    private Stack<IView> _views = new();
    private ViewName _defaultViewName = ViewName.DatabaseMenu;
    private bool _running = true;
    private readonly IViewFactory _viewFactory;

    public ViewManagerService(IViewFactory viewFactory)
    {
        _viewFactory = viewFactory;
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
        return _viewFactory.CreateView(viewName);
    }
}

public enum ViewName
{
    MainMenu,
    DatabaseMenu,
    OpenFolder,
    BrowseImages,
    Vote,
    ImageDetails
}