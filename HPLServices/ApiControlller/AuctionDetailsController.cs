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
    public class AuctionDetailsController : ApiController
    {
        private HPLEntities db = new HPLEntities();

        // GET: api/AuctionDetails
        public IQueryable<AuctionDetail> GetAuctionDetails()
        {
            return db.AuctionDetails;
        }

        // GET: api/AuctionDetails/5
        [ResponseType(typeof(AuctionDetail))]
        public IHttpActionResult GetAuctionDetail(int id)
        {
            AuctionDetail auctionDetail = db.AuctionDetails.Find(id);
            if (auctionDetail == null)
            {
                return NotFound();
            }

            return Ok(auctionDetail);
        }

        // PUT: api/AuctionDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuctionDetail(int id, AuctionDetail auctionDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auctionDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(auctionDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionDetailExists(id))
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

        // POST: api/AuctionDetails
        [ResponseType(typeof(AuctionDetail))]
        public IHttpActionResult PostAuctionDetail(AuctionDetail auctionDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AuctionDetails.Add(auctionDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = auctionDetail.Id }, auctionDetail);
        }

        // DELETE: api/AuctionDetails/5
        [ResponseType(typeof(AuctionDetail))]
        public IHttpActionResult DeleteAuctionDetail(int id)
        {
            AuctionDetail auctionDetail = db.AuctionDetails.Find(id);
            if (auctionDetail == null)
            {
                return NotFound();
            }

            db.AuctionDetails.Remove(auctionDetail);
            db.SaveChanges();

            return Ok(auctionDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuctionDetailExists(int id)
        {
            return db.AuctionDetails.Count(e => e.Id == id) > 0;
        }
    }
}