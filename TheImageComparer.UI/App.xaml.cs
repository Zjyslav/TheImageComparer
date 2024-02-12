using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TheImageComparer.UI.ViewModels;
using TheImageComparer.UI.Views;

namespace TheImageComparer.UI;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IHost? AppHost { get; private set; }

    public App()
    {
        var builder = Host.CreateApplicationBuilder();

        // Add services here
        builder.Services.AddSingleton<ShellView>();
        builder.Services.AddSingleton<ShellViewModel>();

        AppHost = builder.Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var shell = AppHost.Services.GetRequiredService<ShellView>();
        shell.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}

