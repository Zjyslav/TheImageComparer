using Microsoft.EntityFrameworkCore;
using TheImageComparer.Logic.Models;
using TheImageComparer.Logic.Services;

namespace TheImageComparer.Logic.Data;
public class ImageComparerDataAccess : IImageComparerDataAccess
{
    private readonly ImageComparerDbContextFactory _contextFactory;
    private readonly IGetSqliteDbFilePath _dbFilePath;
    private ImageComparerDbContext? _dbContext;
    private List<ImageModel>? _images;
    private List<VoteModel>? _votes;

    public ImageComparerDataAccess(ImageComparerDbContextFactory contextFactory, IGetSqliteDbFilePath dbFilePath)
    {
        _contextFactory = contextFactory;
        _dbFilePath = dbFilePath;
        
    }
    public List<ImageModel> GetAllImages()
    {
        if (_images is null)
        {
            InitDbContext();
            _images = _dbContext!.Images.ToList();
        }

        return _images;
    }

    public List<VoteModel> GetAllVotes()
    {
        if (_votes is null)
        {
            InitDbContext();
            _votes = _dbContext!
                .Votes
                .Include(v => v.VotedFor)
                .Include(i => i.VotedAgainst)
                .ToList();
        }

        return _votes;
    }

    public ImageModel? GetImageById(int id)
    {
        return GetAllImages().FirstOrDefault(i => i.Id == id);
    }
    public bool ImageAlreadyAdded(string filePath)
    {
        return GetAllImages().Any(i => i.FilePath == filePath);
    }

    public IEnumerable<ImageModel> AddImages(IEnumerable<string> filePaths)
    {
        InitDbContext();
        var images = filePaths.Select(f => new ImageModel() { FilePath = f });
        _dbContext!.Images.AddRange(images);
        _dbContext!.SaveChanges();
        _images = null;
        return images;
    }

    public IEnumerable<VoteModel> GetVotesByImageId(int id)
    {
        return GetAllVotes()
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
        _votes = null;
    }

    private void InitDbContext()
    {
        if (_dbContext is not null)
            return;

        if (_dbFilePath.DbFilePath is not null)
            _dbContext = _contextFactory.CreateDbContext(_dbFilePath.DbFilePath);
    }
}
