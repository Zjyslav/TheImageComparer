using Microsoft.EntityFrameworkCore;
using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;

namespace TheImageComparer.Logic.Data;
public class ImageComparerDataAccess : IImageComparerDataAccess
{
    private readonly ImageComparerDbContextFactory _contextFactory;
    private readonly IGetSqliteDbFilePath _dbFilePath;

    public ImageComparerDataAccess(ImageComparerDbContextFactory contextFactory, ISqliteDbFilePathService dbFilePathService)
    {
        _contextFactory = contextFactory;
        _dbFilePath = dbFilePathService;
    }
    public List<ImageModel> GetAllImages()
    {
        if (_dbFilePath.DbFilePath is null)
            return [];
        using var db = _contextFactory.CreateDbContext(_dbFilePath.DbFilePath);
        return db.Images.AsNoTracking().ToList();
    }
}
