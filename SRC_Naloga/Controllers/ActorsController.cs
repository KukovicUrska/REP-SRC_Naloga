using Microsoft.AspNetCore.Mvc;
using SRC_Naloga.DBConnection;

namespace SRC_Naloga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        [HttpGet("GetActors")]
        public IActionResult GetActors()
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var actor = (from a in db.Actors
                                  select a).ToList();
                    return Ok(actor);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Actors pagination support
        [HttpGet("GetActorsPagination")]
        public IActionResult GetActorsPagination(int pageNumber = 1, int pageSize = 4)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var actors = (from a in db.Actors
                                  select a).OrderBy(d => d.BornDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    return Ok(actors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Actor by ID with movies
        [HttpGet("GetActorByID")]
        public IActionResult GetActorByID(string id)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var query = (from a in db.Actors
                                 join am in db.ActorMovies on a.IdActor equals am.FkIdActor
                                 join m in db.Movies on am.FkIdMovie equals m.IdMovie
                                 where a.IdActor == id
                                 select m).ToList();

                    var response = new
                    {
                        Actor = (from a in db.Actors 
                                 where a.IdActor == id 
                                 select a).ToList(),
                        Movies = query
                    };

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("PostActor")]
        public IActionResult PostActor([FromBody] Actor actor)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    db.Actors.Add(actor);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("PutActor")]
        public IActionResult PutActor(Actor actor)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var a = db.Actors.Where(x => x.IdActor == actor.IdActor).FirstOrDefault();
                    if (a != null)
                    {
                        db.Entry(a).CurrentValues.SetValues(actor);

                        db.SaveChanges();
                        return Ok(a);
                    }
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("DeleteActor")]
        public IActionResult DeleteActor(string id)
        {
            try
            {
                using (DbImdbContext db = new DbImdbContext())
                {
                    var actor = (from a in db.Actors
                                 where a.IdActor == id
                                 select a).FirstOrDefault();
                    db.Remove(actor);
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
