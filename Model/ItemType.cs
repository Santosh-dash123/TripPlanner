using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Model
{
    [Table("TPItem_Types")]
    public class item_types
    {
        public int? id { get; set; }
        public string? type_name { get; set; }
    }
}
