using System.Threading.Tasks;
using Bytescout.Pdf.Api.Services;
using Bytescout.Pdf.DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bytescout.Pdf.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly ApplicationDbContext _context;

        public AccountController(JwtService jwtService, ApplicationDbContext context)
        {
            _jwtService = jwtService;
            _context = context;
        }
        
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody]string username)
        {
            // TODO: remove temp stub and use normal _userManager request by username:password
            var identity = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username." });
            }

            var token = _jwtService.GenerateSecurityToken(identity.Email);

            return new OkObjectResult(new {token = token});
        }
    }
}
