using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core2API.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private TicketContext _context;

        public TicketController(TicketContext context)
        {
            _context = context;

            // Inserting demo data
            if (!_context.TicketItems.Any())
            {
                _context.TicketItems.Add(new TicketItem { Consert = "Beyonce" });
                _context.SaveChanges();
            }
        } 

        [HttpGet]
        public IEnumerable<TicketItem> GetAll()
        {
            return _context.TicketItems.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetTicket")]  // Name is route name
        public IActionResult GetById(long id)
        {
            TicketItem ticket = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            { 
                return NotFound();  // 404
            }
            return new ObjectResult(ticket);  // 200
        }

        [HttpPost]
        public IActionResult Create([FromBody] TicketItem ticket)
        {
            if (ticket == null)
            {
                return BadRequest(); // 400
            }
            _context.TicketItems.Add(ticket);
            _context.SaveChanges();

            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
        }

        [HttpPut]
        public IActionResult Update(long id, [FromBody] TicketItem ticket)
        {
            if (ticket == null || ticket.Id != id)
            {
                return BadRequest();  // 400
            }
            var tic = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (tic == null)
            {
                return NotFound(); // 404
            }
            tic.Consert = ticket.Consert;
            tic.Available = ticket.Available;
            tic.Artist = ticket.Artist;

            _context.TicketItems.Update(tic);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ticket = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound(); // 404
            }
            _context.TicketItems.Remove(ticket);
            _context.SaveChanges();
            return new NoContentResult();
        }

        /*
         * 200 series - OK
         * 300 series - changed
         * 400 probably client fault
         * 500 probably server fault
         */
    }
}