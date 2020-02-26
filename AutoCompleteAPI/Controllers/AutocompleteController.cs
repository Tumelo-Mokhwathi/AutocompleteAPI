using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoCompleteAPI.Models;

namespace AutoCompleteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutocompleteController : ControllerBase
    {
        private readonly AutocompleteContext _context;

        public AutocompleteController(AutocompleteContext context)
        {
            _context = context;
        }

        // GET: api/Autocomplete
        [HttpGet]
        public IEnumerable<Autocomplete> GetAutocomplete()
        {
            return _context.Autocomplete;
        }

        // GET: api/Autocomplete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAutocomplete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autocomplete = await _context.Autocomplete.FindAsync(id);

            if (autocomplete == null)
            {
                return NotFound();
            }

            return Ok(autocomplete);
        }

        // PUT: api/Autocomplete/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutocomplete([FromRoute] int id, [FromBody] Autocomplete autocomplete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autocomplete.Id)
            {
                return BadRequest();
            }

            _context.Entry(autocomplete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutocompleteExists(id))
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

        // POST: api/Autocomplete
        [HttpPost]
        public async Task<IActionResult> PostAutocomplete([FromBody] Autocomplete autocomplete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Autocomplete.Add(autocomplete);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAutocomplete", new { id = autocomplete.Id }, autocomplete);
        }

        // DELETE: api/Autocomplete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutocomplete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autocomplete = await _context.Autocomplete.FindAsync(id);
            if (autocomplete == null)
            {
                return NotFound();
            }

            _context.Autocomplete.Remove(autocomplete);
            await _context.SaveChangesAsync();

            return Ok(autocomplete);
        }

        private bool AutocompleteExists(int id)
        {
            return _context.Autocomplete.Any(e => e.Id == id);
        }
    }
}