using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Model
{
    [Table("trips")]
    public class Trip
    {
        [Column("id")]
        public int? Id { get; set; }

        [Column("trip_name")]
        public string? TripName { get; set; }

        [Column("location")]
        public string? Location { get; set; }

        [Column("trip_date")]
        public DateTimeOffset? TripDate { get; set; }

        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}