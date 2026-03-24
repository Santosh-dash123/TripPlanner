using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.DTO
{
    public class ExpenseSummaryDto
    {
        public string? item_name { get; set; }
        public decimal? amount { get; set; }
        public int? total_members { get; set; }
        public decimal? per_person { get; set; }
        public DateTimeOffset? created_at { get; set; }
    }
}
