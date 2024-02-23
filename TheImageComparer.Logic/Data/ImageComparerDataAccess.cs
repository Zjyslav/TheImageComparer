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
    public bool ImageAlreadyAdded(string filePath)
    {
        if (_dbFilePath.DbFilePath is null)
            return false;
        using var db = _contextFactory.CreateDbContext(_dbFilePath.DbFilePath);
        return db.Images.Any(i => i.FilePath == filePath);
    }

    public List<ImageModel> AddImages(IEnumerable<string> filePaths)
    {
        if (_dbFilePath.DbFilePath is null)
            return [];
        using var db = _contextFactory.CreateDbContext(_dbFilePath.DbFilePath);
        var images = filePaths.Select(f => new ImageModel() { FilePath = f });
        db.Images.AddRange(images);
        db.SaveChanges();
        return images.ToList();
    }

    public List<VoteModel> GetVotesByImageId(int id)
    {
        if (_dbFilePath.DbFilePath is null)
            return [];
        using var db = _contextFactory.CreateDbContext(_dbFilePath.DbFilePath);
        return db.Votes
            .AsNoTracking()
            .Where(v => v.VotedFor.Id == id || v.VotedAgainst.Id == id)
            .ToList();
    }
}
