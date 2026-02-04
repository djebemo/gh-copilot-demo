using albums_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace albums_api.Controllers
{
    [Route("albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        /// <summary>
        /// Liefert alle Alben zurück.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Album>> Get()
        {
            var albums = Album.GetAll();

            if (albums == null || !albums.Any())
            {
                return NotFound("Keine Alben gefunden.");
            }

            return Ok(albums);
        }

        /// <summary>
        /// Liefert Alben für die angegebene Id. Optionally sort results by title, artist or price.
        /// Erlaubte Werte für `sortBy`: "title", "artist", "price".
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Album>> Get(int id, [FromQuery] string? sortBy = null)
        {
            if (id <= 0)
            {
                return BadRequest("Album ID must be a positive number.");
            }

            var albums = Album.GetAll()?.Where(a => a.Id == id) ?? Enumerable.Empty<Album>();

            if (!albums.Any())
            {
                return NotFound($"No album found with id {id}.");
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var key = sortBy.Trim().ToLowerInvariant();

                albums = key switch
                {
                    "title" => albums.OrderBy(a => a.Title),
                    "artist" => albums.OrderBy(a => a.Artist),
                    "price" => albums.OrderBy(a => a.Price),
                    _ => null
                };

                if (albums == null)
                {
                    return BadRequest("Invalid sortBy value. Allowed values: title, artist, price.");
                }
            }

            return Ok(albums);
        }

    }
}
