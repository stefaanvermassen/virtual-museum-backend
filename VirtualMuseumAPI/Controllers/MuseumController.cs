﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VirtualMuseumAPI.Helpers;
using VirtualMuseumAPI.Models;
using Microsoft.AspNet.Identity;
using System.Data.Linq;
using System.IO;
using System.Net.Http.Headers;

namespace VirtualMuseumAPI.Controllers
{
    [Authorize]
    public class MuseumController : ApiController
    {
        
        /// <summary>
        /// Get the serialized binary file that is assigned to the museum with the specified id
        /// </summary>
        /// <param name="id">The Museum's unique ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/Museum/{id}/data")]
        [HttpGet]
        public HttpResponseMessage GetMuseumData(int id)
        {
            VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
            if (!dc.Museums.Any(a => a.ID == id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The museum doesn't exist.");
            }
            else
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                Binary bin = dc.Museums.First(p => p.ID == id).Data;
                MemoryStream stream = new MemoryStream(bin.ToArray());
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return result;
            }
        }

        /// <summary>
        /// Get the properties of a random museum. You have to get the binary data via the /data.
        /// </summary>
        /// <param name="id">The Museum's unique ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/Museum/random")]
        [HttpGet]
        public HttpResponseMessage GetMuseumData()
        {
            VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
            Random rand = new Random();
            int toSkip = rand.Next(0, dc.Museums.Count());
            return Get(dc.Museums.Skip(toSkip).Take(1).First().ID);            
        }

        // GET api/museum/id
        /// <summary>
        /// Get the properties of the museum with the specified id
        /// </summary>
        /// <param name="id">The Museum's unique ID</param>
        /// <returns>The museum object with the specified id</returns>
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
        /// <summary>
        /// Creates a new Virtual Museum
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The newly created museum object</returns>
        public HttpResponseMessage Post(MuseumModel model)
        {
            if (ModelState.IsValid)
            {
                VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
                VirtualMuseumFactory factory = new VirtualMuseumFactory();
                Museum museum = factory.createMuseum(model.Description, Privacy.Levels.Private, User.Identity, User.Identity);
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
        /// <summary>
        /// Assigning a serialized binary file to the Museum with the specified id
        /// </summary>
        /// <param name="id">The Museum's unique ID</param>
        /// <returns>The museum object with the specified id, the file has been assigned.</returns>
        public async Task<HttpResponseMessage> PostAsync(int id)
        {
            VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
            if (!dc.Museums.Any(a => a.ID == id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The museum doesn't exist.");
            }
            if (ModelState.IsValid)
            {
                if (Request.Content.IsMimeMultipartContent())
                {
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);
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

                    return Request.CreateResponse(HttpStatusCode.OK, MuseumModel);
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

        // PUT api/Museum/id
        /// <summary>
        /// Edit the Museum's properties
        /// </summary>
        /// <param name="id">The Museum's unique ID</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, MuseumModel model)
        {
            if (ModelState.IsValid)
            {
                VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
                if (!dc.Museums.Any(a => a.ID == id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The museum doesn't exist.");
                }
                else
                {
                    PrivacyLevel privacyLevel = dc.PrivacyLevels.Where(a => a.Name == "PRIVATE").First();
                    if (dc.PrivacyLevels.Any(a => a.Name == Enum.GetName(typeof(Privacy.Levels), (int) model.Privacy)))
                    {
                        privacyLevel = dc.PrivacyLevels.Where(a => a.Name == Enum.GetName(typeof(Privacy.Levels), (int)model.Privacy)).First();
                    }
                   
                    Museum museum = dc.Museums.First(a => a.ID == id);
                    museum.PrivacyLevelID = privacyLevel.ID;
                    museum.Description = model.Description;
                    museum.ModiBy = User.Identity.GetUserId();
                    museum.ModiDate =  DateTime.Now;
                    dc.SubmitChanges();
                    model.LastModified = museum.ModiDate;
                    model.MuseumID = museum.ID;               
                    return Request.CreateResponse(HttpStatusCode.OK, model);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

        }
    }
}
