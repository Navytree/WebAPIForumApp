using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIForumApp.Data;
using WebAPIForumApp.DTOs.Posts;
using WebAPIForumApp.DTOs.Topics;
using WebAPIForumApp.Models;

namespace WebAPIForumApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly WebAPIForumAppContext _context;

        public PostsController(WebAPIForumAppContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p=>p.Id == id);
            if (post == null)
            { return NotFound();}

            var dto = new PostDTO
            {
                Id = post.Id,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                AuthorName = post.User.Login
            };

            return dto;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, CreatePostDTO dto)
        {
            var originalPost = await _context.Posts.FindAsync(id);
            if(originalPost == null) {  return NotFound();}
            if (originalPost.UserId != dto.UserId) return Forbid();

            originalPost.Content = dto.Content;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(CreatePostDTO dto)
        {
            var post = new Post
            {
                Content = dto.Content,
                CreatedAt = DateTime.Now,
                UserId = dto.UserId,
                TopicId = dto.TopicId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            await _context.Entry(post).Reference(t => t.User).LoadAsync();

            var resultDto = new PostDTO
            {
                Id = post.Id,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                AuthorName = post.User.Login
            };

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, resultDto);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id, [FromBody] CreatePostDTO dto)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) { return NotFound(); }
            if (post.UserId != dto.UserId) return Forbid();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
