using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIForumApp.Data;
using WebAPIForumApp.DTOs.Topics;
using WebAPIForumApp.DTOs.Posts;
using WebAPIForumApp.Models;

namespace WebAPIForumApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly WebAPIForumAppContext _context;

        public TopicsController(WebAPIForumAppContext context)
        {
            _context = context;
        }

        // GET: api/Topics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetTopics()
        {
            var topics = await _context.Topics
                .Select(topic => new TopicDTO
                {
                    Id = topic.Id,
                    Title = topic.Title,
                    AuthorName = topic.User.Login
                }).ToListAsync();

            return Ok(topics);
        }

        // GET: api/Topics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDTO>> GetTopic(int id)
        {
            var topic = await _context.Topics
                    .Include(t => t.User)
                    .Include(t => t.Posts)
                        .ThenInclude(p => p.User)
                    .FirstOrDefaultAsync(t => t.Id == id);

            if (topic == null) return NotFound();

            var dto = new TopicDTO
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                CreatedAt = topic.CreatedAt,
                AuthorName = topic.User.Login,
                Posts = topic.Posts.Select(p => new PostDTO
                {
                    Id = p.Id,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    AuthorName = p.User.Login
                }).ToList()
            };

            return dto;
        }

        // PUT: api/Topics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopic(int id, CreateTopicDTO dto)
        {
            var originalTopic = await _context.Topics.FindAsync(id);
            if (originalTopic == null) return NotFound();
            if (originalTopic.UserId != dto.UserId) return Forbid();

            originalTopic.Title = dto.Title;
            originalTopic.Description = dto.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Topics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TopicDTO>> PostTopic(CreateTopicDTO dto)
        {
            var topic = new Topic
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                UserId = dto.UserId
            };

            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            await _context.Entry(topic).Reference(t => t.User).LoadAsync();

            var resultDto = new TopicDTO
            {
                Id = topic.Id,
                Title = topic.Title,
                AuthorName = topic.User.Login,
                Description = topic.Description,
                CreatedAt = topic.CreatedAt,
                Posts = []
            };

            return CreatedAtAction(nameof(GetTopic), new { id = topic.Id }, resultDto);

        }

        // DELETE: api/Topics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id, [FromBody] CreateTopicDTO dto)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null) return NotFound();
            if (topic.UserId != dto.UserId) return Forbid();

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }

    }
}
