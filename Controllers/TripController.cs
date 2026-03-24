using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.DTO;
using TripPlanner.Model;

namespace TripPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TripController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTrip([FromBody] TripCreateDto model)
        {
            if (model == null || model.Members == null || !model.Members.Any())
            {
                return BadRequest("Invalid data");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var trip = new Trip
                {
                    TripName = model.TripName,
                    Location = model.Location,
                    TripDate = model.TripDate,
                    CreatedBy = model.CreatedBy
                };

                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();

                var users = model.Members.Select(m => new User
                {
                    Name = m.Name,
                    Email = m.Email,
                    Password = "member@1234",
                    Role = m.Role
                }).ToList();

                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();

                var members = model.Members.Select((m, index) => new Member
                {
                    TripId = trip.Id,
                    Name = m.Name,
                    Email = m.Email,
                    Address = m.Address,
                    Role = m.Role,
                    UserId = users[index].Id
                }).ToList();

                await _context.Members.AddRangeAsync(members);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "Trip Created Successfully",
                    tripId = trip.Id
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "Something went wrong",
                    error = ex.Message
                });
            }
        }
    }
}
