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

    public ICollection<DailySongStats> DailyStats { get; set; } = new List<DailySongStats>();

}