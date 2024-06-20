using Microsoft.Extensions.DependencyInjection;
using TheImageComparer.UI.Factories;
using TheImageComparer.UI.Services;
using TheImageComparer.UI.ViewModels;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI.Helpers.StartupHelpers;
public static class ServiceExtensions
{
    public static void AddViewsAndViewModels(this IServiceCollection services)
    {
        services.AddSingleton<ShellView>();
        services.AddSingleton<ShellViewModel>();

        services.AddFromFactory<MainMenuView>();
        services.AddTransient<MainMenuViewModel>();

        services.AddFromFactory<DatabaseMenuView>();
        services.AddTransient<DatabaseMenuViewModel>();

        services.AddFromFactory<OpenFolderView>();
        services.AddTransient<OpenFolderViewModel>();

        services.AddFromFactory<BrowseImagesView>();
        services.AddTransient<BrowseImagesViewModel>();

        services.AddFromFactory<VoteView>();
        services.AddTransient<VoteViewModel>();

        services.AddFromFactory<VoteStatsView>();
        services.AddTransient<VoteStatsViewModel>();

        services.AddScoped<IViewFactory, ViewFactory>();
    }

    public static void AddViewManagerService(this IServiceCollection services)
    {
        services.AddScoped<IViewManagerService, ViewManagerService>();
    }

    public static void AddFromFactory<T>(this IServiceCollection services)
        where T : class
    {
        services.AddTransient<T>();
        services.AddScoped<Func<T>>(x => () => x.GetService<T>()!);
        services.AddScoped<IAbstractFactory<T>, AbstractFactory<T>>();
    }
}
