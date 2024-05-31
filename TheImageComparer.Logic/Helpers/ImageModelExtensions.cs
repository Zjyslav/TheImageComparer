using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Helpers;
public static class ImageModelExtensions
{
    public static int GetVotesForCount(this ImageModel image) => image.VotesFor.Count;
    public static int GetVotesAgainstCount(this ImageModel image) => image.VotesAgainst.Count;
    public static int GetVotesCount(this ImageModel image) => image.GetVotesForCount() + image.GetVotesAgainstCount();
    public static int GetScore(this ImageModel image) => (image.GetVotesForCount() * 1000) / image.GetVotesCount();
    public static List<VoteModel> GetAllVotes(this ImageModel image) => image.VotesFor.Concat(image.VotesAgainst).ToList();
}
