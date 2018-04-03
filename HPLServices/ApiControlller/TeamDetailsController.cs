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
    public class TeamDetailsController : ApiController
    {
        private HPLEntities db = new HPLEntities();

        // GET: api/TeamDetails
        public IQueryable<TeamDetail> GetTeamDetails()
        {
            return db.TeamDetails;
        }

        // GET: api/TeamDetails/5
        [ResponseType(typeof(TeamDetail))]
        public IHttpActionResult GetTeamDetail(int id)
        {
            TeamDetail teamDetail = db.TeamDetails.Find(id);
            if (teamDetail == null)
            {
                return NotFound();
            }

            return Ok(teamDetail);
        }

        // PUT: api/TeamDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeamDetail(int id, TeamDetail teamDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(teamDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamDetailExists(id))
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

        // POST: api/TeamDetails
        [ResponseType(typeof(TeamDetail))]
        public IHttpActionResult PostTeamDetail(TeamDetail teamDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeamDetails.Add(teamDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teamDetail.Id }, teamDetail);
        }

        // DELETE: api/TeamDetails/5
        [ResponseType(typeof(TeamDetail))]
        public IHttpActionResult DeleteTeamDetail(int id)
        {
            TeamDetail teamDetail = db.TeamDetails.Find(id);
            if (teamDetail == null)
            {
                return NotFound();
            }

            db.TeamDetails.Remove(teamDetail);
            db.SaveChanges();

            return Ok(teamDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamDetailExists(int id)
        {
            return db.TeamDetails.Count(e => e.Id == id) > 0;
        }
    }
}