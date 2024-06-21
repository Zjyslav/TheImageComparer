using TheImageComparer.UI.Services;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI.Factories;
public interface IViewFactory
{
    IView? CreateView(ViewName viewName);
}