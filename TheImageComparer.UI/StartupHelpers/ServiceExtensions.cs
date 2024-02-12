using Microsoft.Extensions.DependencyInjection;
using TheImageComparer.UI.Services;
using TheImageComparer.UI.ViewModels;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI.StartupHelpers;
public static class ServiceExtensions
{
    public static void AddViewsAndViewModels(this IServiceCollection services)
    {
        services.AddSingleton<ShellView>();
        services.AddSingleton<ShellViewModel>();

        services.AddFromFactory<MainMenuView>();
        services.AddTransient<MainMenuViewModel>();
    }

    public static void AddViewManagerService(this IServiceCollection services)
    {
        services.AddSingleton<IViewManagerService, ViewManagerService>();
    }

    public static void AddFromFactory<T>(this IServiceCollection services)
        where T : class
    {
        services.AddTransient<T>();
        services.AddSingleton<Func<T>>(x => () => x.GetService<T>()!);
        services.AddSingleton<IAbstractFactory<T>, AbstractFactory<T>>();
    }
}
