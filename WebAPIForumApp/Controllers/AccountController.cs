using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIForumApp.Data;
using WebAPIForumApp.DTOs.User;
using WebAPIForumApp.Models;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{

        private readonly WebAPIForumAppContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AccountController(WebAPIForumAppContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (await _context.Users.AnyAsync(u => u.Login == dto.Login))
                { return BadRequest("Login is already taken."); }

            var user = new User
            {
                Login = dto.Login,
                Email = dto.Email,
                PasswordHash = _passwordHasher.Hash(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", "Users", new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == dto.Login);

            if (user == null || !_passwordHasher.Verify(dto.Password, user.PasswordHash))
            { return Unauthorized("Invalid login or password."); }

            return Ok(new { Message = "Logged in successfully", UserId = user.Id });
        }



}
