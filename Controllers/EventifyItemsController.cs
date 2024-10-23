using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventify.Models;
using EventifyApi.Models;

namespace Eventify.Controllers
{
    [Route("api/EventifyItems")]
    [ApiController]
    public class EventifyItemsController : ControllerBase
    {
        private readonly EventifyContext _context;

        public EventifyItemsController(EventifyContext context)
        {
            _context = context;
        }

        // GET: api/EventifyItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventifyItem>>> GetEventifyItems()
        {
            return await _context.EventifyItems.ToListAsync();
        }

        // GET: api/EventifyItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventifyItem>> GetEventifyItem(long id)
        {
            var eventifyItem = await _context.EventifyItems.FindAsync(id);

            if (eventifyItem == null)
            {
                return NotFound();
            }

            return eventifyItem;
        }

        [HttpGet("ObterPorNome")]
        public ActionResult<EventifyItem> GetEventifyItemName(string nome)
        {
            var eventifyItem = _context.EventifyItems.Where(x => x.Nome.Contains(nome));

            if (eventifyItem == null)
            {
                return NotFound();
            }

            return Ok(eventifyItem);
        }

        // PUT: api/EventifyItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventifyItem(long id, EventifyItem eventifyItem)
        {
            if (id != eventifyItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventifyItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventifyItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EventifyItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventifyItem>> PostEventifyItem(EventifyItem eventifyItem)
        {
            _context.EventifyItems.Add(eventifyItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventifyItem), new { id = eventifyItem.Id }, eventifyItem);
        }

        // DELETE: api/EventifyItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventifyItem(long id)
        {
            var eventifyItem = await _context.EventifyItems.FindAsync(id);
            if (eventifyItem == null)
            {
                return NotFound();
            }

            _context.EventifyItems.Remove(eventifyItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventifyItemExists(long id)
        {
            return _context.EventifyItems.Any(e => e.Id == id);
        }
    }
}
