using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Model
{
    [Table("TPExpenses")]
    public class Expense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }
        [Column("trip_id")]
        public int? TripId { get; set; }
        [Column("added_by")]
        public int? AddedBy { get; set; }
        [Column("item_name")]
        public string? ItemName { get; set; }
        [Column("item_type_id")]
        public int? ItemTypeId { get; set; }
        [Column("amount")]
        public decimal? Amount { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}
