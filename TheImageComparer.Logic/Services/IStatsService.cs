using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Services;
public interface IStatsService
{
    Task<VoteStatsModel> GetVoteStats();
}