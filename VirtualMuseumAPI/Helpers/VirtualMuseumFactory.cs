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
        VirtualMuseumDataContext dc = new VirtualMuseumDataContext();

        public Artwork createArtWork(byte[] buffer, IIdentity modiByUser)
        {            
            Artwork artwork = new Artwork
            {
                ArtistID = 1,
                name = "",
                ModiBy = IdentityExtensions.GetUserId(modiByUser),
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

        public Artist createArtist(String name, IIdentity modiByUser, IIdentity artistUser = null)
        {
            Artist artist = new Artist
            {
                Name = name,
                UID = (artistUser != null)? IdentityExtensions.GetUserId(modiByUser): null,
                ModiBy = IdentityExtensions.GetUserId(modiByUser),
                ModiDate = DateTime.Now
            };
            dc.Artists.InsertOnSubmit(artist);
            dc.SubmitChanges();
            return artist;
        }
    }
}