﻿using System;
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
using LinqKit;
using System.Linq.Expressions;

namespace VirtualMuseumAPI.Controllers
{
    [Authorize]
    public class ArtWorkController : ApiController
    {
        VirtualMuseumDataContext dc;

        public ArtWorkController()
        {
            dc = new VirtualMuseumDataContext();
        }
        public class ArtworkResults
        {
            public IEnumerable<ArtWorkModel> ArtWorks { get; set; }
        }


        /// <summary>
        /// Get all the artworks that match the specified fields via the query arguments. Please use as: <![CDATA[api/artwork?name=UGent&ArtistID=1&filter[0].Name=tag&filter[0].Value=cool]]>
        /// </summary>
        /// <param name="query">Filter fields</param>
        /// <returns></returns>
        public ArtworkResults Get([FromUri] ArtWorkSearchModel query)
        {
            string userID = User.Identity.GetUserId();
            var predicate = PredicateBuilder.True<Artwork>();
            if (String.IsNullOrEmpty(query.Name) && query.Filter == null && query.ArtistID != 0)
            {
                //No filters provided, return the union from all your filters
                foreach (ArtworkFilter filter in dc.ArtworkFilters.Where(a => a.ArtworkFiltersXUsers.Any(b => b.UID == userID)))
                {
                    predicate = predicate.Or(p => p.ArtworkMetadatas.Any(
                        q => q.ArtworkKey.ID == filter.ArtworkKeyID && q.Value == filter.Value));
                }
            }
            else
            {
                //Search in public art
                //TODO predicate to public art
                if (!String.IsNullOrEmpty(query.Name)) predicate = predicate.And(p => p.name == query.Name);

                if (query.ArtistID != 0) predicate = predicate.And(p => p.ArtistID == query.ArtistID);

                if (query.Filter != null)
                {
                    foreach (KeyValuePair kv in query.Filter.Where(a => a.Name != null
                    && dc.ArtworkKeys.Any(k => k.name.ToLower() == a.Name.Trim().ToLower()) && a.Value != null))
                    {
                        predicate = predicate.And(p => p.ArtworkMetadatas.Any(
                            q => q.ArtworkKey.ID == dc.ArtworkKeys.Where(r => r.name == kv.Name).First().ID && q.Value == kv.Value));
                    }
                }
            }
            
            List<ArtWorkModel> artworks = new List<ArtWorkModel>();
            foreach (Artwork work in dc.Artworks.Where(predicate))
            {
                ArtWorkModel model = new ArtWorkModel();
                model.ArtistID = work.ArtistID;
                model.ArtWorkID = work.ID;
                model.Name = work.name;
                artworks.Add(model);
            }
            
            ArtworkResults result = new ArtworkResults()
            {
                ArtWorks = artworks
            };
         
            return result;

        }
        
        /// <summary>
        /// Get an JPG image file that is assigned to the artwork with the specified id
        /// </summary>
        /// <param name="id">The Museum's unique ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/Artwork/{id}/data")]
        [HttpGet]
        public IHttpActionResult GetArtworkData(int id)
        {
            if (!dc.ArtworkRepresentations.Any(a => a.ArtworkID == id))
            {
                return NotFound();
            }
            else
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                Binary bin = dc.ArtworkRepresentations.FirstOrDefault(p => p.ArtworkID == id).Data;
                MemoryStream stream = new MemoryStream(bin.ToArray());
                return new VirtualMuseumDataResult(stream, ("image/jpg"));
            }
            
        }

        // GET api/Artwork/id
        /// <summary>
        /// Get the properties of the artwork with the specified id
        /// </summary>
        /// <param name="id">The Artworks's unique ID</param>
        /// <returns>The artwork object with the specified id</returns>
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {

            if (!dc.Artworks.Any(a => a.ID == id))
            {
                return NotFound();
            }
            else
            {
                Artwork artwork = dc.Artworks.First(p => p.ID == id);
                ArtWorkModel model = new ArtWorkModel();
                model.ArtistID = artwork.ArtistID;
                model.Name = artwork.name;
                model.ArtWorkID = artwork.ID;
                return Ok(model);
            }
        }

        // POST api/ArtWork
        /// <summary>
        /// Create a new Artwork and add an JPEG image file to it
        /// </summary>
        /// <returns>The ID of the newly created artwork. You can edit the properties via the PUT method</returns>
        public async Task<IHttpActionResult> PostAsync()
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
                        Artwork artwork = VMFactory.CreateArtWork(buffer, User.Identity);
                        ArtWorkModel artworkModel = new ArtWorkModel()
                        {
                            ArtistID = artwork.ArtistID,
                            ArtWorkID = artwork.ID,
                            Name = artwork.name
                        };
                        messages.Add(artworkModel);
                    }
                    ArtworkResults result = new ArtworkResults()
                    {
                        ArtWorks = messages
                    };
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // PUT api/ArtWork/5
        /// <summary>
        /// Edit the ArtWork properties
        /// </summary>
        /// <param name="id">The ArtWork's unique ID</param>
        /// <param name="work"></param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, ArtWorkModel work)
        {
            if (ModelState.IsValid)
            {
                if (!dc.ArtworkRepresentations.Any(a => a.ArtworkID == id))
                {
                    return NotFound();
                }
                else
                {
                    if (!dc.Artists.Any(a => a.ID == work.ArtistID))
                    {
                        return NotFound();
                    }
                    Artwork artWork = dc.Artworks.FirstOrDefault(a => a.ID == id);
                    work.ArtWorkID = id;
                    artWork.name = work.Name;
                    artWork.ModiBy = User.Identity.GetUserId();
                    artWork.ModiDate = DateTime.Now;
                    dc.SubmitChanges();
                    return Ok(work);
                } 
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

         //DELETE api/ArtWork/5
        public IHttpActionResult Delete(int id)
        {
            if (!dc.ArtworkRepresentations.Any(a => a.ArtworkID == id))
            {
                return NotFound();
            }
            else
            {
                var artWorkMetaData = dc.ArtworkMetadatas.Where(a => a.ArtworkID == id);
                foreach (var metaDataItem in artWorkMetaData)
                {
                    dc.ArtworkMetadatas.DeleteOnSubmit(metaDataItem);
                }
                var artWorkRepresentations = dc.ArtworkRepresentations.Where(a => a.ArtworkID == id);
                foreach (var representationItem in artWorkRepresentations)
                {
                    dc.ArtworkRepresentations.DeleteOnSubmit(representationItem);
                }

                var artWorkInMuseums = dc.MuseumsXArtworks.Where(a => a.ArtworkID == id);
                foreach (var museumItem in artWorkInMuseums)
                {
                    dc.MuseumsXArtworks.DeleteOnSubmit(museumItem);
                }

                Artwork artWork = dc.Artworks.First(a => a.ID == id);
                dc.Artworks.DeleteOnSubmit(artWork);
                dc.SubmitChanges();
                return Ok();
            } 
        }

       
    }
}
