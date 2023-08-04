using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Davies_Forum_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {

        private readonly DataContext context;

        public ForumController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Forum>>> Get()
        {
            return Ok(await this.context.ForumEntries.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Forum>> Get(int id)
        {
            var entry = await this.context.ForumEntries.FindAsync(id);
            if (entry == null)
                return BadRequest("Entry not found");
            return Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult<List<Forum>>> AddEntry(Forum entry)
        {
            this.context.ForumEntries.Add(entry);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.ForumEntries.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Forum>>> UpdateEntry(Forum request)
        {
            var dbEntry = await this.context.ForumEntries.FindAsync(request.Id);
            if (dbEntry == null)
            {
                return BadRequest("Entry not found");
            }

            dbEntry.Name = request.Name;
            dbEntry.Title = request.Title;
            dbEntry.Description = request.Description;
            dbEntry.Office = request.Office;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.ForumEntries.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Forum>>> Delete(int id)
        {
            var dbEntry = await this.context.ForumEntries.FindAsync(id);
            if (dbEntry == null)
            {
                return BadRequest("Entry not found");
            }

            this.context.ForumEntries.Remove(dbEntry);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.ForumEntries.ToListAsync());
        }
    }
}