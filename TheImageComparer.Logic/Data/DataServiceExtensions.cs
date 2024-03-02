using Microsoft.Extensions.DependencyInjection;
using TheImageComparer.Logic.Services;

namespace TheImageComparer.Logic.Data;
public static class DataServiceExtensions
{
    public static void AddSqliteDataAccess(this IServiceCollection services)
    {
        services.AddScoped<ISqliteDbFilePathService, SqliteDbFilePathService>();
        services.AddScoped<ISetSqliteDbFilePath>(provider => provider.GetRequiredService<ISqliteDbFilePathService>());
        services.AddScoped<IGetSqliteDbFilePath>(provider => provider.GetRequiredService<ISqliteDbFilePathService>());
        services.AddSingleton<ImageComparerDbContextFactory>();
        services.AddScoped<IImageComparerDataAccess, ImageComparerDataAccess>();
    }
}
