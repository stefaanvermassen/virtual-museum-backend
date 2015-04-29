using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirtualMuseumAPI.Models;
using Microsoft.AspNet.Identity;
using VirtualMuseumAPI.Helpers;

namespace VirtualMuseumAPI.Controllers
{
    [Authorize]
    public class ArtWorkFilterController : ApiController
    {
        VirtualMuseumDataContext dc;

        public ArtWorkFilterController()
        {
            dc = new VirtualMuseumDataContext();
        }
        public class ArtWorkFilterResults
        {
            public IEnumerable<KeyValuePair> ArtWorkFilters { get; set; }
        }

        /// <summary>
        /// Get all the filters that are assigned to a specific user
        /// </summary>
        /// <returns></returns>
        [Route("api/ArtWorkFilter/connected")]
        [HttpGet]
        public ArtWorkFilterResults Get()
        {
            string userID = User.Identity.GetUserId();
            List<KeyValuePair> filters = new List<KeyValuePair>();
            foreach (ArtworkFilter filter in dc.ArtworkFilters.Where(a => a.ArtworkFiltersXUsers.Any(b => b.UID == userID)))
            {
                filters.Add(new KeyValuePair() { Name = filter.ArtworkKey.name, Value = filter.Value });
            }
            ArtWorkFilterResults result = new ArtWorkFilterResults()
            {
                ArtWorkFilters = filters
            };
            return result;
        }

        /// <summary>
        /// Search for a specific filter
        /// </summary>
        /// <param name="pair">Name or Value</param>
        /// <returns>Array of found filters</returns>
        public ArtWorkFilterResults Get([FromUri] KeyValuePair pair)
        {
            List<KeyValuePair> filters = new List<KeyValuePair>();
            if ((!String.IsNullOrEmpty(pair.Name)) && String.IsNullOrEmpty(pair.Value))
            {
                foreach (ArtworkFilter filter in dc.ArtworkFilters.Where(a => a.ArtworkKey.name.ToLower() == pair.Name.Trim().ToLower()))
                {
                    filters.Add(new KeyValuePair() {Id=filter.ID, Name = filter.ArtworkKey.name, Value = filter.Value });
                }
                return new ArtWorkFilterResults() { ArtWorkFilters = filters };
            }
            else if ((!String.IsNullOrEmpty(pair.Value)) && String.IsNullOrEmpty(pair.Name))
            {
                foreach (ArtworkFilter filter in dc.ArtworkFilters.Where(a => a.Value.ToLower() == pair.Value.Trim().ToLower()))
                {
                    filters.Add(new KeyValuePair() { Id = filter.ID, Name = filter.ArtworkKey.name, Value = filter.Value });
                }
            }
            else if ((!String.IsNullOrEmpty(pair.Value)) && (!String.IsNullOrEmpty(pair.Name)))
            {
                foreach (ArtworkFilter filter in dc.ArtworkFilters.Where(a => a.Value.ToLower() == pair.Value.Trim().ToLower() && a.ArtworkKey.name.ToLower() == pair.Name.Trim().ToLower()))
                {
                    filters.Add(new KeyValuePair() { Id = filter.ID, Name = filter.ArtworkKey.name, Value = filter.Value });
                } 
            }
            
            return new ArtWorkFilterResults() { ArtWorkFilters = filters };
        }

        /// <summary>
        /// Assign a filter to the user, makes a new one if the filter doesn't already exist
        /// </summary>
        /// <param name="pair">The name and the value of the filter, e.g. Genre, nsfw</param>
        /// <returns>The filter that is assigned</returns>
        public IHttpActionResult Post(KeyValuePair pair)
        {
            VirtualMuseumFactory factory = new VirtualMuseumFactory();
            if (String.IsNullOrEmpty(pair.Name) || String.IsNullOrEmpty(pair.Value))
            {
                return BadRequest();
            }

            if (dc.ArtworkFilters.Any(a => a.Value.ToLower() == pair.Name.Trim().ToLower() && a.ArtworkKey.name.ToLower() == pair.Name.Trim().ToLower()))
            {
                ArtworkFilter filter = dc.ArtworkFilters.First(a => a.Value.ToLower() == pair.Name.Trim().ToLower() && a.ArtworkKey.name.ToLower() == pair.Name.Trim().ToLower());
                factory.AssignArtWorkFilterToUser(filter.ID, User.Identity);
                return Ok(new KeyValuePair() { Id = filter.ID, Name = filter.ArtworkKey.name, Value = filter.Value });
            }

            //If key does not exist, make a new
            ArtworkKey key = null;
            if (!dc.ArtworkKeys.Any(a => a.name.ToLower() == pair.Name.Trim().ToLower()))
            {
                key = factory.CreateArtworkKey(pair.Name.ToLower().Trim());
            }else{
                key = dc.ArtworkKeys.First(a => a.name.ToLower() == pair.Name.Trim().ToLower());
            }

            ArtworkFilter newFilter = factory.CreateArtWorkFilter(key.ID, pair.Value, User.Identity);
            factory.AssignArtWorkFilterToUser(newFilter.ID, User.Identity);
            return Ok(new KeyValuePair() { Id = newFilter.ID, Name = newFilter.ArtworkKey.name, Value = newFilter.Value });
        }

        /// <summary>
        /// Deletes an existing filter from the current user. If the filter is not assigned to other user, it will be deleted completely
        /// </summary>
        /// <param name="id">The ArtworkFilter's unique ID</param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            String userId = User.Identity.GetUserId();
            if(!dc.ArtworkFilters.Any(a=> a.ID == id))
            {
                return NotFound();
            }
            if (dc.ArtworkFiltersXUsers.Any(a => a.UID == userId && a.ArtworkFilterID == id)){
                dc.ArtworkFiltersXUsers.DeleteOnSubmit(dc.ArtworkFiltersXUsers.First(a => a.UID == userId && a.ArtworkFilterID == id));
                dc.SubmitChanges();
            }else{
                return NotFound();
            }

            //If not assigned to other user, delete filter
            if (dc.ArtworkFiltersXUsers.Any(a => a.ArtworkFilterID == id))
            {
                dc.ArtworkFilters.DeleteOnSubmit(dc.ArtworkFilters.First());
                dc.SubmitChanges();
            }
            return Ok();
        }

    }
}
