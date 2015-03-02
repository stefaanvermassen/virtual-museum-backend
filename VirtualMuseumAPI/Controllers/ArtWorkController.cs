using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VirtualMuseumAPI.Controllers
{
    [Authorize]
    public class ArtWorkController : ApiController
    {
        // GET api/ArtWork
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/ArtWork/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/ArtWork
        public void Post([FromBody]string value)
        {
        }

        // PUT api/ArtWork/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/ArtWork/5
        public void Delete(int id)
        {
        }
    }
}
