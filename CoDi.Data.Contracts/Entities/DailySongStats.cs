using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoDi.Data.Contracts.Entities;

public class DailySongStats
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("song_id")]
    public int SongId { get; set; }

    [Column("day")]
    public DateOnly Day { get; set; }

    [Column("first_played_at")]
    public TimeOnly FirstPlayedAt { get; set; }

    [Column("time_played_sec")]
    public TimeSpan TimePlayedSec { get; set; }

    public Song Song { get; set; } = null!;

}