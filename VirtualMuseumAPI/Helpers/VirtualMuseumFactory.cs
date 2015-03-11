using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualMuseumAPI.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;


namespace VirtualMuseumAPI.Helpers
{
    public class VirtualMuseumFactory
    {
        public Artwork createArtWork(IIdentity user, byte[] buffer)
        {
            VirtualMuseumDataContext dc = new VirtualMuseumDataContext();
            Artwork artwork = new Artwork
            {
                ArtistID = 1,
                name = "",
                ModiBy = IdentityExtensions.GetUserId(user),
                ModiDate = DateTime.Now
            };
            dc.Artworks.InsertOnSubmit(artwork);
            dc.SubmitChanges();
            ArtworkRepresentation representation = new ArtworkRepresentation
            {
                ArtworkID = artwork.ID,
                DataGUID = Guid.NewGuid(),
                Data = new System.Data.Linq.Binary(buffer),
                Size = 1
            };
            dc.ArtworkRepresentations.InsertOnSubmit(representation);
            dc.SubmitChanges();
            return artwork;
        } 
    }
}