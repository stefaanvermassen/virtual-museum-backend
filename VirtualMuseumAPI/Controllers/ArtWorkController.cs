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
using VirtualMuseumAPI.Helpers.Filters;
using System.Data.Linq;
using VirtualMuseumAPI.Helpers;

namespace VirtualMuseumAPI.Controllers
{
    [Authorize]
    public class ArtWorkController : ApiController
    {

        public class ArtworkResults
        {
            public IEnumerable<ArtWorkModel> ArtWorks { get; set; }
        }

        // GET api/ArtWork
        [AllowAnonymous]
        public ArtworkResults Get([FromUri] ArtWorkSearchModel query)
        {
            List<ArtWorkModel> artworks = new List<ArtWorkModel>();
            VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
            foreach(Artwork work in dc.Artworks){
                ArtWorkModel model = new ArtWorkModel();
                model.ArtistID = work.ArtistID;
                model.ArtWorkID = work.ID;
                model.Name = work.name;
                artworks.Add(model);
            }

            IEnumerable<ArtWorkModel> filteredArtworks = artworks;

            if (!String.IsNullOrEmpty(query.Name))
                filteredArtworks = filteredArtworks.Where(p => p.Name == query.Name);

            if (query.ArtistID != 0)
                filteredArtworks = filteredArtworks.Where(p => p.ArtistID == query.ArtistID);
            ArtworkResults result = new ArtworkResults()
            {
                ArtWorks = filteredArtworks
            };
         
            return result;

        }



        // GET api/ArtWork/5
        [AllowAnonymous]
        public HttpResponseMessage Get(int id)
        {
            VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
            if (!dc.ArtworkRepresentations.Any(a => a.ArtworkID == id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The artwork doesn't exist.");
            }
            else
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                Binary bin = dc.ArtworkRepresentations.FirstOrDefault(p => p.ArtworkID == id).Data;
                MemoryStream stream = new MemoryStream(bin.ToArray());
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                return result;
            }
            
        }

        // POST api/ArtWork
        public async Task<HttpResponseMessage> PostAsync()
        {
            if (ModelState.IsValid)
            {
                // Do something with the product (not shown).
                if (Request.Content.IsMimeMultipartContent())
                {
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);

                    List<ArtWorkModel> messages = new List<ArtWorkModel>();
                    foreach (var file in provider.Contents)
                    {
                        var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                        var buffer = await file.ReadAsByteArrayAsync();
                        VirtualMuseumFactory VMFactory = new VirtualMuseumFactory();
                        Artwork artwork = VMFactory.createArtWork(buffer, User.Identity);
                        ArtWorkModel artworkModel = new ArtWorkModel()
                        {
                            ArtistID = artwork.ArtistID,
                            ArtWorkID = artwork.ID,
                            Name = artwork.name
                        };
                        messages.Add(artworkModel);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, messages);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

        }

        // PUT api/ArtWork/5
        public HttpResponseMessage Put(int id, ArtWorkModel work)
        {
            if (ModelState.IsValid)
            {
                VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
                if (!dc.ArtworkRepresentations.Any(a => a.ArtworkID == id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The artwork doesn't exist.");
                }
                else
                {
                    if (!dc.Artists.Any(a => a.ID == work.ArtistID))
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The artist doesn't exist.");
                    }
                    Artwork artWork = dc.Artworks.FirstOrDefault(a => a.ID == id);
                    work.ArtWorkID = id;
                    artWork.name = work.Name;
                    artWork.ModiBy = User.Identity.GetUserId();
                    artWork.ModiDate = DateTime.Now;
                    dc.SubmitChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, work);
                } 
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

        }

        // DELETE api/ArtWork/5
        public void Delete(int id)
        {
        }

       
    }
}
