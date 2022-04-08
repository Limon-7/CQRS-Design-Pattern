using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence;
using APP.API.Contracts.V1;

namespace APP.API.Controllers.V1
{
   
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet(ApiRoutes.Values.GetAll)]
        public async Task<ActionResult<IEnumerable<Value>>> GetValues()
        {
            return await _context.Values.ToListAsync();
        }


        [HttpGet(ApiRoutes.Values.Get)]
        public async Task<ActionResult<Value>> GetValue([FromRoute]Guid valueId)
        {
            var value = await _context.Values.FindAsync(valueId);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        // PUT: api/Values/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut(ApiRoutes.Values.Update)]
        public async Task<IActionResult> PutValue(Guid valueId, Value value)
        {
            if (valueId != value.Id)
            {
                return BadRequest();
            }

            _context.Entry(value).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValueExists(valueId))
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

        // POST: api/Values
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(ApiRoutes.Values.Create)]
        public async Task<ActionResult<Value>> PostValue([FromBody]Value title)
        {
            var value = new Value
            {
                Title = title.Title
            };
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            var baseUrl=$"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            Console.WriteLine($"baseUrl:{baseUrl}");
            Console.WriteLine($"value.Id:{value.Id}");
            var locationUrl = baseUrl + "/" + ApiRoutes.Values.Get.Replace("{valueId}", value.Id.ToString());
            var response = new Value
            {
                Id=value.Id,
                Title = title.Title
            };
            
            return CreatedAtAction(locationUrl, response);
        }

        // DELETE: api/Values/5
        [HttpDelete(ApiRoutes.Values.Delete)]
        public async Task<IActionResult> DeleteValue(Guid id)
        {
            var value = await _context.Values.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }

            _context.Values.Remove(value);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ValueExists(Guid id)
        {
            return _context.Values.Any(e => e.Id == id);
        }
    }
}
