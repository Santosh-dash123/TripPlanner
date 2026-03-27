using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;
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
                return BadRequest("Invalid data");

            try
            {
                var dt = new DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("Role", typeof(string));
                dt.Columns.Add("User_Id", typeof(int));

                foreach (var m in model.Members)
                {
                    dt.Rows.Add(m.Name, m.Email, m.Address, m.Role, 0);
                }

                using var conn = _context.Database.GetDbConnection();
                await conn.OpenAsync();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "TP_SP_CreateTripWithMembers";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TripName", model.TripName));
                cmd.Parameters.Add(new SqlParameter("@Location", model.Location));
                cmd.Parameters.Add(new SqlParameter("@TripDate", model.TripDate));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", model.CreatedBy));

                var memberParam = new SqlParameter("@Members", dt);
                memberParam.SqlDbType = SqlDbType.Structured;
                memberParam.TypeName = "TP_MemberType";

                cmd.Parameters.Add(memberParam);

                var result = await cmd.ExecuteScalarAsync();

                return Ok(new
                {
                    message = "Trip Created Successfully",
                    tripId = Convert.ToInt32(result)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Something went wrong",
                    error = ex.Message
                });
            }
        }
    }
}
