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
    public IEnumerable<ImageModel> GetAllImages()
    {
        InitDbContext();
        return _dbContext!.Images;
    }

    public ImageModel? GetImageById(int id)
    {
        InitDbContext();
        return _dbContext!.Images.FirstOrDefault(i => i.Id == id);
    }
    public bool ImageAlreadyAdded(string filePath)
    {
        InitDbContext();
        return _dbContext!.Images.Any(i => i.FilePath == filePath);
    }

    public IEnumerable<ImageModel> AddImages(IEnumerable<string> filePaths)
    {
        InitDbContext();
        var images = filePaths.Select(f => new ImageModel() { FilePath = f });
        _dbContext!.Images.AddRange(images);
        _dbContext!.SaveChanges();
        return images.ToList();
    }

    public IEnumerable<VoteModel> GetVotesByImageId(int id)
    {
        InitDbContext();
        return _dbContext!.Votes
            .Include(v => v.VotedFor)
            .Include(i => i.VotedAgainst)
            .Where(v => v.VotedFor.Id == id || v.VotedAgainst.Id == id);
    }
    public void CreateVote(int votedForImageId, int votedAgainstImageId)
    {
        InitDbContext();
        var votedFor = GetImageById(votedForImageId);
        var votedAgainst = GetImageById(votedAgainstImageId);
        if (votedFor is null || votedAgainst is null)
            return;
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
