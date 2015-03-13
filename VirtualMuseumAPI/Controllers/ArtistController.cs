using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Antlr.Runtime;
using Microsoft.AspNet.Identity;
using VirtualMuseumAPI.Models;
using VirtualMuseumAPI.Helpers;

namespace VirtualMuseumAPI.Controllers
{
    [Authorize]
    public class ArtistController : ApiController
    {
        // GET: api/Artist
        /// <summary>
        /// Get all artists in the system
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public IEnumerable<ArtistModel> Get()
        {
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                return dc.Artists.Select(artist => new ArtistModel {ArtistID = artist.ID, Name = artist.Name}).ToList();
            }
        }

        // GET: api/Artist/5
        [AllowAnonymous]
        public ArtistModel Get(int id)
        {
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                var artist = dc.Artists.First(a => a.ID == id);
                if (artist != null)
                {
                    ArtistModel am = new ArtistModel {ArtistID = artist.ID, Name = artist.Name};
                    return am;
                }
                return null;
            }
        }

        // POST: api/Artist
        public HttpResponseMessage Post([FromBody]ArtistModel value)
        {
            if (ModelState.IsValid)
            {
                VirtualMuseumFactory VMFactory = new VirtualMuseumFactory();
                Artist artist = VMFactory.createArtist(value.Name, User.Identity, User.Identity);
                return Request.CreateResponse(HttpStatusCode.OK, artist.ID);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
        }

        // PUT: api/Artist/5
        public void Put(int id, [FromBody]ArtistModel value)
        {
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                var artist = dc.Artists.First(a => a.ID == id);
                if (artist == null)
                {
                    return;
                }
                artist.ID = value.ArtistID;
                artist.Name = value.Name;
                artist.ModiBy = "01732c65-2af1-44a4-93ae-1200745678ae";
                artist.ModiDate = DateTime.Now;

                //dc.Artists.Attach(artist);
                dc.SubmitChanges();
            }
        }

        // DELETE: api/Artist/5
        public void Delete(int id)
        {
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                var artistToDelete = dc.Artists.First(a => a.ID == id);
                if (artistToDelete != null)
                {
                    dc.Artists.DeleteOnSubmit(entity: artistToDelete);
                    dc.SubmitChanges();
                }
            }
        }
    }
}