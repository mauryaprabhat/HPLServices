using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HPLServices;

namespace HPLServices.ApiControlller
{
    public class PlayerDetailsController : ApiController
    {
        private HPLEntities db = new HPLEntities();

        // GET: api/PlayerDetails
        public IQueryable<PlayerDetail> GetPlayerDetails()
        {
            return db.PlayerDetails.Where(x=>x.Status ==1).Take(1);
           // return (from player in db.PlayerDetails where player.Status == 1).Take(1);
        }

        // GET: api/PlayerDetails/5
        [ResponseType(typeof(PlayerDetail))]
        public IHttpActionResult GetPlayerDetail(int id)
        {
            PlayerDetail playerDetail = db.PlayerDetails.Find(id);
            if (playerDetail == null)
            {
                return NotFound();
            }

            return Ok(playerDetail);
        }

        //// PUT: api/PlayerDetails/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutPlayerDetail(int id, PlayerDetail playerDetail)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != playerDetail.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(playerDetail).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PlayerDetailExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // PUT: api/PutStatusPlayer/5  update status of the employee 
        
        [ResponseType(typeof(void))]
        [Route("api/PlayerDetails/{employeeID}")]
        public IHttpActionResult PutStatusPlayer(int employeeID)
        {
            //PlayerDetail playerDetail = db.PlayerDetails.Find(employeeID);
            //if (playerDetail == null)
            //{
            //    return NotFound();
            //}

            //db.playerDetail.emp;
            //db.SaveChanges();
            //return Ok(playerDetail);           

            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new HPLEntities())
            {
                var existingPlayer = ctx.PlayerDetails.Where(s => s.EmployeeId == employeeID)
                                                        .FirstOrDefault<PlayerDetail>();

                if (existingPlayer != null)
                {
                    existingPlayer.Status = 0;

                    ctx.SaveChanges();
                }               
                else
                {
                    return NotFound();
                }
                //var teamPlayerEntry = ctx.TeamDetails.Where(x => x.TeamName.ToUpper() == teamName.ToUpper()).FirstOrDefault<TeamDetail>();
                //if (teamPlayerEntry != null)
                //{
                //    teamPlayerEntry.PlayerCount = teamPlayerEntry.PlayerCount + 1 ;

                //    ctx.SaveChanges();
                //}
                //else
                //{
                //    return NotFound();
                //}

            }

            return Ok();

        }

        // POST: api/PlayerDetails
        [ResponseType(typeof(PlayerDetail))]
        public IHttpActionResult PostPlayerDetail(PlayerDetail playerDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlayerDetails.Add(playerDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = playerDetail.Id }, playerDetail);
        }

        // DELETE: api/PlayerDetails/5
        [ResponseType(typeof(PlayerDetail))]
        public IHttpActionResult DeletePlayerDetail(int id)
        {
            PlayerDetail playerDetail = db.PlayerDetails.Find(id);
            if (playerDetail == null)
            {
                return NotFound();
            }

            db.PlayerDetails.Remove(playerDetail);
            db.SaveChanges();

            return Ok(playerDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerDetailExists(int id)
        {
            return db.PlayerDetails.Count(e => e.Id == id) > 0;
        }
    }
}