using Microsoft.Extensions.DependencyInjection;

namespace TheImageComparer.Logic.Data;
public static class DataServiceExtensions
{
    public static void AddSqliteDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<ImageComparerDbContextFactory>();
        services.AddTransient<IImageComparerDataAccess, ImageComparerDataAccess>();
    }
}
