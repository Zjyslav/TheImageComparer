using System.IO;
using TheImageComparer.Logic.Models;

namespace TheImageComparer.UI.Models;
public class ImageUIModel : ImageModel
{
    public string FileName
    {
        get
        {
            return Path.GetFileName(FilePath);
        }
    }

    public List<VoteModel> Votes { get; set; } = [];
    public List<VoteModel> VotesFor
    {
        get
        {
            return Votes
                .Where(v => v.VotedFor.Id == Id)
                .ToList();
        }
    }
    public List<VoteModel> VotesAgainst
    {
        get
        {
            return Votes
                .Where(v => v.VotedAgainst.Id == Id)
                .ToList();
        }
    }
    public int Score { get; set; }
}
