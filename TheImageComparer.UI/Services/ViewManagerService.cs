using System.Windows.Controls;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI.Services;
public class ViewManagerService
{
    private readonly ContentControl _shellViewer;
    private Stack<IView> _views = new();

    public ViewManagerService(ShellView shellView)
    {
        _shellViewer = shellView.ShellViewer;
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

    private void ShowInShellViewer()
    {
        _shellViewer.Content = _views.Peek();
    }

    private IView? CreateView(ViewName viewName)
    {
        switch (viewName)
        {
            default:
                return null;
        }
    }
}

public enum ViewName
{
    
}