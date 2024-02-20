using Microsoft.Extensions.DependencyInjection;
using TheImageComparer.Logic.Services;

namespace TheImageComparer.Logic.Data;
public static class DataServiceExtensions
{
    public static void AddSqliteDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<ISqliteDbFilePathService, SqliteDbFilePathService>();
        services.AddSingleton<ImageComparerDbContextFactory>();
        services.AddTransient<IImageComparerDataAccess, ImageComparerDataAccess>();
    }
}
