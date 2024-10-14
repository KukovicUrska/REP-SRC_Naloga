using Microsoft.AspNetCore.Mvc;
using SRC_Naloga.DBConnection;
using System.Dynamic;

namespace SRC_Naloga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet("GetMovies")]
        public IActionResult GetMovies()
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var movies = (from m in db.Movies
                                  select m).ToList();
                    return Ok(movies);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Movies pagination support
        [HttpGet("GetMoviesPagination")]
        public IActionResult GetMoviesPagination(int pageNumber = 1, int pageSize = 4)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var movies = (from m in db.Movies
                                  select m).OrderBy(d => d.Year).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    return Ok(movies);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //Movies (and actors) Search
        [HttpGet("GetSearchMovie")]
        public IActionResult GetSearchMovie(string searchTerm)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var moviesSearch = (from m in db.Movies
                                        where m.Title.Contains(searchTerm) || m.Year.ToString().Contains(searchTerm)
                                        select m).ToList();

                    return Ok(moviesSearch);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Movie by ID with actors
        [HttpGet("GetMovieByID")]
        public IActionResult GetMovieByID(string id)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var query = (from m in db.Movies
                                 join am in db.ActorMovies on m.IdMovie equals am.FkIdMovie
                                 join a in db.Actors on am.FkIdActor equals a.IdActor
                                 where m.IdMovie == id
                                 select a).ToList();

                    var response = new
                    {
                        Movie = (from m in db.Movies
                                 where m.IdMovie == id
                                 select m).ToList(),
                        Actors = query
                    };

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("PostMovie")]
        public IActionResult PostMovie([FromBody] DBConnection.Movie movie)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("PutMovie")]
        public IActionResult PutMovie(DBConnection.Movie movie)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var m = db.Movies.Where(x => x.IdMovie == movie.IdMovie).FirstOrDefault();
                    if (m != null)
                    {
                        db.Entry(m).CurrentValues.SetValues(movie);

                        db.SaveChanges();
                        return Ok(m);
                    }
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("DeleteMovie")]
        public IActionResult DeleteMovie(string id)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var movie = (from m in db.Movies
                                 where m.IdMovie == id
                                 select m).FirstOrDefault();
                    db.Remove(movie);
                    db.SaveChanges();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
