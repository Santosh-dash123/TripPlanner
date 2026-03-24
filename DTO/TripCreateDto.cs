namespace TripPlanner.DTO
{
    public class TripCreateDto
    {
        public string? TripName { get; set; }
        public string? Location { get; set; }
        public DateTimeOffset? TripDate { get; set; }
        public int? CreatedBy { get; set; }
        public List<MemberDto>? Members { get; set; }
    }

}
