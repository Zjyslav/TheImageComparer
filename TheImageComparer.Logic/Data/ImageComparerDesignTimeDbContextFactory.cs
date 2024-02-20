using Microsoft.EntityFrameworkCore.Design;

namespace TheImageComparer.Logic.Data;
public class ImageComparerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ImageComparerDbContext>
{
    public ImageComparerDbContext CreateDbContext(string[] args)
    {
        string path = Path.Join(Directory.GetCurrentDirectory(), "Resources/database.db");
        return new ImageComparerDbContext(path);
    }
}
