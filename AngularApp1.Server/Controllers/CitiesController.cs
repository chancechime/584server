using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModel;
using AngularApp1.Server.DTO;
using System.Diagnostics.Metrics;

namespace AngularApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController(ElmosworldContext context) : ControllerBase
    {
        //primary constructor is a paramater to the class
        private readonly ElmosworldContext _context = context;

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IList<CityDTO>>> GetCities()
        {
            //IQueryable knows what db you're using
            //kinda like a compiler to change the string to a SQL statement

            //IEnumerable is when you open a connection to the db and get the
            //data from the db

            //IList or IArray or ... is a derived type of IEnumerable
            //and can be the returned type

            //IQueryable -> IEnumerable -> IList/IArray/...
            IQueryable<CityDTO> x = _context.Cities.Select((c) =>
                new CityDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Population = c.Population,
                    Ascii = c.Ascii,
                    Lng = c.Lng,
                    Lat = c.Lat,
                    // c.Country gives us the country associated with the city
                    CountryName = c.Country.Name,
                }).Take(100); //will only give us 100 rows

            return await x.ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            City? city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        //Put == update
        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // Post == create
        // POST: api/Cities 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            City? city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}