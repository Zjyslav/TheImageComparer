using Microsoft.EntityFrameworkCore;

namespace TheImageComparer.Logic.Data;
public class ImageComparerDbContextFactory
{
    public ImageComparerDbContext CreateDbContext(string dbFilePath)
    {
        return new ImageComparerDbContext(dbFilePath);
    }
}
