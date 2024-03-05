namespace TheImageComparer.Logic.Models;
public class VoteModel
{
    public int Id { get; set; }
    public required ImageModel VotedFor { get; set; }
    public required ImageModel VotedAgainst { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
}
