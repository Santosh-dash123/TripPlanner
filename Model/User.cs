using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Model
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int? Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("role")]
        public string? Role { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}
