using System.ComponentModel.DataAnnotations.Schema;

namespace TheImageComparer.Logic.Models;
public class ImageModel
{
    public int Id { get; set; }
    public required string FilePath { get; set; }
    public bool IsArchived { get; set; } = false;
    public bool PossibleDuplicate { get; set; } = false;
    public ICollection<VoteModel> VotesFor { get; set; } = new List<VoteModel>();
    public ICollection<VoteModel> VotesAgainst { get; set; } = new List<VoteModel>();
}
