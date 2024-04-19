using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Data;

public interface IImageComparerDataAccess
{
    Task<IEnumerable<ImageModel>> AddImages(IEnumerable<string> filePaths);
    Task CreateVote(int votedForImageId, int votedAgainstImageId);
    IQueryable<ImageModel> GetAllImages();
    IQueryable<VoteModel> GetAllVotes();
    Task<ImageModel?> GetImageById(int id);
    Task<IEnumerable<VoteModel>> GetVotesByImageId(int id);
    Task<bool> ImageAlreadyAdded(string filePath);
}