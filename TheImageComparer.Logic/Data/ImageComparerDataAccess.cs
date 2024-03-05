using Microsoft.EntityFrameworkCore;
using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;

namespace TheImageComparer.Logic.Data;
public class ImageComparerDataAccess : IImageComparerDataAccess
{
    private readonly ImageComparerDbContextFactory _contextFactory;
    private readonly IGetSqliteDbFilePath _dbFilePath;
    private ImageComparerDbContext? _dbContext;

    public ImageComparerDataAccess(ImageComparerDbContextFactory contextFactory, IGetSqliteDbFilePath dbFilePath)
    {
        _contextFactory = contextFactory;
        _dbFilePath = dbFilePath;
        
    }
    public List<ImageModel> GetAllImages()
    {
        InitDbContext();
        return _dbContext!.Images.ToList();
    }
    public bool ImageAlreadyAdded(string filePath)
    {
        InitDbContext();
        return _dbContext!.Images.Any(i => i.FilePath == filePath);
    }

    public List<ImageModel> AddImages(IEnumerable<string> filePaths)
    {
        InitDbContext();
        var images = filePaths.Select(f => new ImageModel() { FilePath = f });
        _dbContext!.Images.AddRange(images);
        _dbContext!.SaveChanges();
        return images.ToList();
    }

    public List<VoteModel> GetVotesByImageId(int id)
    {
        InitDbContext();
        return _dbContext!.Votes
            .Where(v => v.VotedFor.Id == id || v.VotedAgainst.Id == id)
            .ToList();
    }
    public void CreateVote(ImageModel votedFor, ImageModel votedAgainst)
    {
        InitDbContext();
        _dbContext!.Votes.Add(new() { VotedFor = votedFor, VotedAgainst = votedAgainst});
        _dbContext!.SaveChanges();
    }

    private void InitDbContext()
    {
        if (_dbContext is not null)
            return;

        if (_dbFilePath.DbFilePath is not null)
            _dbContext = _contextFactory.CreateDbContext(_dbFilePath.DbFilePath);
    }

}
