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
        // To Get details of all players.
        public IQueryable<PlayerDetail> GetPlayerDetails()
        {
            return db.PlayerDetails;
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

        // PUT: api/PlayerDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayerDetail(int id, PlayerDetail playerDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(playerDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
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