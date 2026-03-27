using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripPlanner.DTO;
using TripPlanner.Model;

namespace TripPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpenseController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddExpense([FromBody] Expense model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Invalid data");

                model.CreatedAt = DateTime.Now;
                await _context.Expenses.AddAsync(model);
                await _context.SaveChangesAsync();

                return Ok("Expense Added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error adding expense",
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("summary/{tripId}")]
        public async Task<IActionResult> GetSummary(int tripId)
        {
            try
            {
                var data = await _context.Set<ExpenseSummaryDto>()
                    .FromSqlRaw("EXEC TP_sp_GetTripSummary @TripId = {0}", tripId)
                    .AsNoTracking()
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error fetching summary",
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("getitemtypes")]
        public async Task<IActionResult> GetItemTypes()
        {
            try
            {
                var data = await _context.item_types.AsNoTracking().ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error fetching item types",
                    error = ex.Message
                });
            }
        }
    }
}
