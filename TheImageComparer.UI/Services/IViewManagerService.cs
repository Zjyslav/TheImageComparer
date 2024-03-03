using System.Windows.Controls;

namespace TheImageComparer.UI.Services;

public interface IViewManagerService
{
    void CloseView();
    void OpenView(ViewName viewName);
    public Task StartAsync(ContentControl viewer);
}