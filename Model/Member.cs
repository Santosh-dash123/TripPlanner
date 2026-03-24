using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Model
{
    [Table("members")]
    public class Member
    {
        [Column("id")]
        public int? Id { get; set; }
        [Column("trip_id")]
        public int? TripId { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("role")]
        public string? Role { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
    }
}
