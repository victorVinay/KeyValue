using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KeyValue.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace KeyValue.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KeysController : ControllerBase
    {
        private readonly KeyContext _context;

        public KeysController(KeyContext context)
        {
            _context = context;
        }

        // GET: api/Keys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Key>>> GetKeys()
        {
            return await _context.Keys.ToListAsync();
        }

        // GET: api/Keys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Key>> GetKey(string id)
        {
            var key = await _context.Keys.FindAsync(id);

            if (key == null)
            {
                return NotFound();
            }

            return key;
        }

        // PUT: api/Keys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKey(string id, Key key)
        {
            if (id != key.key)
            {
                return BadRequest();
            }

            _context.Entry(key).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyExists(id))
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

        // POST: api/Keys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Key>> PostKey(Key key)
        {
            if (KeyExists(key.key))
             {
                return Conflict();
            }
            _context.Keys.Add(key);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKey), new { id = key.key }, key);
        }

        // DELETE: api/Keys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKey(string id)
        {
            var key = await _context.Keys.FindAsync(id);
            if (key == null)
            {
                return NotFound();
            }

            _context.Keys.Remove(key);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KeyExists(string id)
        {
            return _context.Keys.Any(e => e.key == id);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateValue(string id, [FromBody] JsonPatchDocument<Key> patchEntity )
        {
            var key = await _context.Keys.FindAsync(id);
            if (key == null)
                return NotFound();
            patchEntity.ApplyTo(key, ModelState);
            await _context.SaveChangesAsync();
            return Ok(key);

        }
    }
}
