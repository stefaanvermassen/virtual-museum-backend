using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VirtualMuseumAPI.Helpers;
using VirtualMuseumAPI.Models;
using Microsoft.AspNet.Identity;

namespace VirtualMuseumAPI.Controllers
{
    public class MuseumController : ApiController
    {

     

        // GET api/museum/id
        [AllowAnonymous]
        public HttpResponseMessage Get(int id)
        {
            VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
            if (!dc.Museums.Any(a => a.ID == id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The museum doesn't exist.");
            }
            else
            {
                Museum museum = dc.Museums.First(p => p.ID == id);
                MuseumModel model = new MuseumModel();
                model.Description = museum.Description;
                model.LastModified = museum.ModiDate;
                model.MuseumID = museum.ID;
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
        }


        // POST api/Museum
        public HttpResponseMessage Post(MuseumModel model)
        {
            if (ModelState.IsValid)
            {
                VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
                VirtualMuseumFactory factory = new VirtualMuseumFactory();
                Museum museum = factory.createMuseum(model.Description, User.Identity, User.Identity);
                model.MuseumID = museum.ID;
                model.LastModified = museum.ModiDate;
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // POST api/Museum/id
        public async Task<HttpResponseMessage> PostAsync(int id)
        {
            VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
            if (!dc.Museums.Any(a => a.ID == id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The museum doesn't exist.");
            }
            if (ModelState.IsValid)
            {
                // Do something with the product (not shown).
                if (Request.Content.IsMimeMultipartContent())
                {
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);
                    List<MuseumModel> messages = new List<MuseumModel>();
                    var file = provider.Contents.First();
                    var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                    var buffer = await file.ReadAsByteArrayAsync();
                    Museum museum = dc.Museums.Where(a=> a.ID == id).First();
                    museum.Data = buffer;
                    museum.ModiBy = User.Identity.GetUserId();
                    dc.Museums.Where(a=> a.ID == id).First().Data = buffer;
                    dc.SubmitChanges();
                    MuseumModel MuseumModel = new MuseumModel(){
                        Description = museum.Description,
                        LastModified = museum.ModiDate,
                        MuseumID = museum.ID
                    };
                    messages.Add(MuseumModel);
                    
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
    }
}
