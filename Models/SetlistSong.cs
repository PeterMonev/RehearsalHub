using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RehearsalHub.Models
{
    public class SetlistSong
    {
        [Required]

        public int SetlistId { get; set; }

        [ForeignKey(nameof(SetlistId))]
        public Setlist Setlist { get; set; } = null!;

        [Required]

        public int SongId { get; set; }

        [ForeignKey(nameof(SongId))]
        public Song Song { get; set; } = null!;
        
    }
}
