using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using VirtualMuseumAPI.Models;

namespace VirtualMuseumAPI.Controllers
{
    public class ArtistController : ApiController
    {
        // GET: api/Artist
        public IEnumerable<Artist> Get()
        {
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                return dc.Artists;
            }
        }

        // GET: api/Artist/5
        public Artist Get(int id)
        {
            using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
            {
                var artist = dc.Artists.First(a => a.ID == id);
                if (artist != null)
                {
                    return artist;
                }
                return null;
            }
        }

        // POST: api/Artist
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Artist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Artist/5
        public void Delete(int id)
        {
        }
    }
}
