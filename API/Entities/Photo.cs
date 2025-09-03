using System.Text.Json.Serialization;

namespace API.Entities;

public class Photo
{
    public int Id { get; set; }

    public string MemberId { get; set; } = null!;
    public required string Url { get; set; }
    public string? PublicId { get; set; }

    // Nav Props
    [JsonIgnore]
    public Member Member { get; set; } = null!;

}
