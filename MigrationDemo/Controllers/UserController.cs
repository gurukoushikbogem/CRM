using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Models;
using MigrationDemo.Services;
using MigrationDemo.Filters;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var (token, user) = await _userService.LoginAsync(request);

                return Ok(new
                {
                    Token = token,
                    User = new
                    {
                        Id = user.UserId,
                        Username = user.Username,
                        Email = user.Email,
                        Role = user.Role,
                        LastLoginAt = user.LastLoginAt
                    }
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                await _userService.Register(request);
                return Ok(new { Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPatch("update-status")]
        [JwtValidation]
        public async Task<IActionResult> UpdateStatus(UpdateStatusRequest request)
        {
            try
            {
                var result = await _userService.UpdateUserStatus(request);
                if (result)
                    return Ok(new { Message = "User status updated successfully" });
                return NotFound(new { Message = "User not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{userId}")]
        [JwtValidation]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var result = await _userService.DeleteUser(userId);

                if (result)
                    return Ok(new { Message = "User deleted successfully" });

                return NotFound(new { Message = "User not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("search")]
        [JwtValidation]
        public async Task<IActionResult> SearchUsers(string query)
        {
            var users = await _userService.SearchUsers(query);
            return Ok(users);
        }


        [HttpGet("role/{role}")]
        [JwtValidation]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            var users = await _userService.GetUserByRole(role);
            return Ok(users);
        }

        [HttpGet("salesperson-id")]
        [JwtValidation]
        public async Task<IActionResult> GetSalespersonIdByName([FromQuery] string name)
        {
            try
            {
                var id = await _userService.GetSalespersonIdByName(name);
                if (id.HasValue)
                    return Ok(new { SalespersonId = id.Value });

                return NotFound(new { Message = "Salesperson not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("sales-report")]
        [JwtValidation]
        public async Task<IActionResult> GenerateSalesReport(DateTime startDate, DateTime endDate)
        {
            var salespeople = await _userService.GetUsersByRole("Sales Representative");

            var filteredSalespeople = salespeople
                .Where(u => u.CreatedAt >= startDate && u.CreatedAt <= endDate)
                .ToList();

            if (!filteredSalespeople.Any())
                return NotFound("No salespersons found in the specified date range.");

            var pdfDocument = new PdfDocument();
            var pdfPage = pdfDocument.AddPage();
            var graphics = XGraphics.FromPdfPage(pdfPage);
            var font = new XFont("Arial", 12, XFontStyle.Regular);

            graphics.DrawString(
                "Sales Report",
                new XFont("Arial", 16, XFontStyle.Bold),
                XBrushes.Black,
                new XPoint(pdfPage.Width / 2, 40)
            );

            graphics.DrawString(
                $"Date Range: {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}",
                font,
                XBrushes.Black,
                new XRect(20, 60, pdfPage.Width - 40, 20),
                XStringFormats.TopLeft
            );

            graphics.DrawString(
                "Username | Email | Role | Created At",
                font,
                XBrushes.Black,
                new XRect(20, 100, pdfPage.Width - 40, 20),
                XStringFormats.TopLeft
            );

            int yPosition = 120;
            foreach (var salesperson in filteredSalespeople)
            {
                graphics.DrawString(
                    $"{salesperson.Username} | {salesperson.Email} | {salesperson.Role} | {salesperson.CreatedAt:yyyy-MM-dd}",
                    font,
                    XBrushes.Black,
                    new XRect(20, yPosition, pdfPage.Width - 40, 20),
                    XStringFormats.TopLeft
                );
                yPosition += 20;

                if (yPosition > pdfPage.Height - 40)
                {
                    pdfPage = pdfDocument.AddPage();
                    graphics = XGraphics.FromPdfPage(pdfPage);
                    yPosition = 20;
                }
            }


            using var stream = new MemoryStream();
            pdfDocument.Save(stream, false);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", $"Sales_Report_{startDate:yyyyMMdd}_to_{endDate:yyyyMMdd}.pdf");
        }

        [HttpGet("username/{username}")]
        [JwtValidation]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUserName(username);
            return user != null ? Ok(user) : NotFound(new { Message = "User not found" });
        }
    }
}
