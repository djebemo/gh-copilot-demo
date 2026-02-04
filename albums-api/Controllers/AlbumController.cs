using albums_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace albums_api.Controllers
{
    [Route("albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        // GET: api/album
        [HttpGet]
        public IActionResult Get()
        {
            var albums = Album.GetAll();

            return Ok(albums);
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery] string? sortBy = null)
        {
            // Retrieve albums by ID (could be multiple if ID is not unique)
            if (id <= 0)
            {
            return BadRequest("Album ID must be a positive number.");
            }

            var albums = Album.GetAll().Where(a => a.Id == id);

            if (!albums.Any())
            {
            return NotFound();
            }

            // Sort albums if sortBy is provided
            albums = sortBy?.ToLower() switch
            {
            "title" => albums.OrderBy(a => a.Title),
            "artist" => albums.OrderBy(a => a.Artist),
            "price" => albums.OrderBy(a => a.Price),
            _ => albums
            };

            return Ok(albums);
        }

    }
}
