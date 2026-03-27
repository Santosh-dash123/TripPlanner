using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.DTO
{
    public class ExpenseSummaryDto
    {
        public string? TripName { get; set; }
        public string? ItemName { get; set; }
        public string? ItemType { get; set; }
        public decimal? Amount { get; set; }
        public int? TotalMembers { get; set; }
        public decimal? PerPersonAmount { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
