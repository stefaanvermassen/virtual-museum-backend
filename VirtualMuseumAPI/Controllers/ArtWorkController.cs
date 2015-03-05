using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VirtualMuseumAPI.Models;
using Microsoft.AspNet.Identity;

namespace VirtualMuseumAPI.Controllers
{
    //[Authorize]
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
       

        public async Task<List<string>> PostAsync()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                List<string> messages = new List<string>();
                foreach (var file in provider.Contents)
                {
                    var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                    var buffer = await file.ReadAsByteArrayAsync();
                    using (VirtualMuseumDataContext dc = new VirtualMuseumDataContext())
                    {
                        Artwork artwork = new Artwork
                        {
                            ArtistID = 1,
                            name = "test",
                            ModiBy = "01732c65-2af1-44a4-93ae-1200745678ae",
                            ModiDate = DateTime.Now
                        };
                        dc.Artworks.InsertOnSubmit(artwork);
                        dc.SubmitChanges();
                        ArtworkRepresentation representation = new ArtworkRepresentation
                        {
                            ArtworkID = artwork.ID,
                            DataGUID = Guid.NewGuid(),
                            Data  = new System.Data.Linq.Binary(buffer),
                            Size = 1
                        };
                        dc.ArtworkRepresentations.InsertOnSubmit(representation);
                        dc.SubmitChanges();
                    }         

                    messages.Add("File uploaded");

                }

                return messages;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                throw new HttpResponseException(response);
            }
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
