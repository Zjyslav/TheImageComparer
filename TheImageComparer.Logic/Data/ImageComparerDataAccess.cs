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
    public IQueryable<ImageModel> GetAllImages()
    {
        return InitAndGetDbContext().Images
            .Include(i => i.VotesFor)
            .Include(i => i.VotesAgainst)
            .AsQueryable();
    }

    public IQueryable<VoteModel> GetAllVotes()
    {
        return InitAndGetDbContext()
            .Votes
            .Include(v => v.VotedFor)
            .Include(i => i.VotedAgainst)
            .AsQueryable();
    }

    public async Task<ImageModel?> GetImageById(int id)
    {
        return await GetAllImages()
            .FirstOrDefaultAsync(i => i.Id == id);
    }
    public async Task<bool> ImageAlreadyAdded(string filePath)
    {
        return await GetAllImages()
            .AnyAsync(i => i.FilePath == filePath);
    }

    public async Task<IEnumerable<ImageModel>> AddImages(IEnumerable<string> filePaths)
    {
        var images = filePaths.Select(f => new ImageModel() { FilePath = f });
        InitAndGetDbContext().Images.AddRange(images);
        await InitAndGetDbContext().SaveChangesAsync();
        return images;
    }

    public async Task<IEnumerable<VoteModel>> GetVotesByImageId(int id)
    {
        return await InitAndGetDbContext().Votes
            .Where(v => v.VotedFor.Id == id || v.VotedAgainst.Id == id)
            .ToListAsync();
    }
    public async Task CreateVote(int votedForImageId, int votedAgainstImageId)
    {
        var votedFor = await GetImageById(votedForImageId);
        var votedAgainst = await GetImageById(votedAgainstImageId);
        if (votedFor is null || votedAgainst is null)
            return;
        InitAndGetDbContext().Votes.Add(new() { VotedFor = votedFor, VotedAgainst = votedAgainst });
        await InitAndGetDbContext().SaveChangesAsync();
    }

    private ImageComparerDbContext InitAndGetDbContext()
    {
        if (_dbContext is not null)
            return _dbContext;

        if (_dbFilePath.DbFilePath is not null)
            _dbContext = _contextFactory.CreateDbContext(_dbFilePath.DbFilePath);

        return _dbContext!;
    }
}
