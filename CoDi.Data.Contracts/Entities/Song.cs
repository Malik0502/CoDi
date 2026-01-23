using System.ComponentModel.DataAnnotations.Schema;

namespace CoDi.Data.Contracts.Entities;

public class Song
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("artist")]
    public required string Artist { get; set; }

    [Column("played_at")]
    public DateTime PlayedAt { get; set; } = DateTime.Now;
}