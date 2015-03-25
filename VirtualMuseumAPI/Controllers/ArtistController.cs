using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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

        public class ArtistResults
        {
            public IEnumerable<ArtistModel> Artists { get; set; }
        }

        // GET api/Artist/connected
        /// <summary>
        /// Get the artists connected to the user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Artist/connected")]
        [HttpGet]
        public HttpResponseMessage GetConnectedArtists()
        {
            string userid = User.Identity.GetUserId();

            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                if (!dc.ArtistsXUsers.Any(a => a.AspNetUser.Id == userid))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The artist doesn't exist.");
                }
                var artists = dc.Artists.Where(artist => artist.AspNetUser.Id == userid).
                    Select(a => new ArtistModel { ArtistID = a.ID, Name = a.Name }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new ArtistResults(){Artists = artists});
            }
        }

        // GET: api/Artist
        /// <summary>
        /// Get all artists in the system
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                var artists = dc.Artists.Select(artist => new ArtistModel {ArtistID = artist.ID, Name = artist.Name}).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new ArtistResults() {Artists = artists});
            }
        }

        // GET: api/Artist/5
        /// <summary>
        /// Get the properties of the artist with the specified id
        /// </summary>
        /// <param name="id">The artist's unique ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        public HttpResponseMessage Get(int id)
        {
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                if (!dc.Artists.Any(a => a.ID == id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The artist doesn't exist.");
                }

                var artist = dc.Artists.First(a => a.ID == id);
                ArtistModel am = new ArtistModel {ArtistID = artist.ID, Name = artist.Name};
                return Request.CreateResponse(HttpStatusCode.OK, am);               
            }
        }

        // POST: api/Artist
        /// <summary>
        /// Create a new public Artist
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]ArtistModel value)
        {
            if (ModelState.IsValid)
            {
                VirtualMuseumFactory VMFactory = new VirtualMuseumFactory();
                Artist artist = VMFactory.CreatePublicArtist(value.Name, User.Identity, User.Identity);
                return Request.CreateResponse(HttpStatusCode.OK, artist.ID);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
        }

        // PUT: api/Artist/5
        /// <summary>
        /// Edit the Artist's properties
        /// </summary>
        /// <param name="id">The Artist's unique ID</param>
        /// <param name="value"></param>
        public HttpResponseMessage Put(int id, [FromBody]ArtistModel value)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                if (!dc.Artists.Any(a => a.ID == id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The artist doesn't exist.");
                }
                var artist = dc.Artists.FirstOrDefault(a => a.ID == id);
                artist.Name = value.Name;
                artist.ModiBy = User.Identity.GetUserId();
                artist.ModiDate = DateTime.Now;

                dc.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new ArtistModel()
                {
                    ArtistID = artist.ID, Name = artist.Name
                });
            }
        }

        // DELETE: api/Artist/5
        /// <summary>
        /// Deletes the specified Artist
        /// </summary>
        /// <param name="id">The Artist's unique ID</param>
        public HttpResponseMessage Delete(int id)
        {
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                var artistToDelete = dc.Artists.First(a => a.ID == id);
                if (artistToDelete != null)
                {
                    dc.Artists.DeleteOnSubmit(entity: artistToDelete);
                    dc.SubmitChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The artist doesn't exist.");
            }
        }
    }
}