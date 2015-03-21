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
                ArtistID = dc.ArtistsXUsers.Where(a => a.UID == IdentityExtensions.GetUserId(modiByUser)).First().ArtistID,
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

        public Artist createPublicArtist(String name, IIdentity modiByUser, IIdentity artistUser = null)
        {
            Artist artist = new Artist
            {
                Name = name,
                ModiBy = IdentityExtensions.GetUserId(modiByUser),
                ModiDate = DateTime.Now
            };
            dc.Artists.InsertOnSubmit(artist);
            dc.SubmitChanges();
            if (artistUser != null)
            {
                ArtistsXUser artistUserEntry = new ArtistsXUser
                {
                    ArtistID = artist.ID,
                    UID = IdentityExtensions.GetUserId(artistUser)
                };
                dc.ArtistsXUsers.InsertOnSubmit(artistUserEntry);
                dc.SubmitChanges();
            }
            
            return artist;
        }

        public Artist createPrivateArtist(String name, String justRegisteredUserID)
        {
            if (dc.AspNetUsers.Any(a => a.Id == justRegisteredUserID))
            {
                Artist artist = new Artist
                {
                    Name = name,
                    ModiBy = justRegisteredUserID,
                    ModiDate = DateTime.Now
                };
                dc.Artists.InsertOnSubmit(artist);
                dc.SubmitChanges();

                    ArtistsXUser artistUserEntry = new ArtistsXUser
                    {
                        ArtistID = artist.ID,
                        UID = justRegisteredUserID
                    };
                    dc.ArtistsXUsers.InsertOnSubmit(artistUserEntry);
                    dc.SubmitChanges();
              
                return artist;
            }
            return null;
        }

        public Museum createMuseum(String description, Privacy.Levels level, IIdentity ownerID, IIdentity modiByUser)
        {
            PrivacyLevel privacyLevel = dc.PrivacyLevels.Where(a=>a.Name == "PRIVATE").First();
            if (dc.PrivacyLevels.Any(a => a.Name == Enum.GetName(typeof(Privacy.Levels), (int) level))){
                privacyLevel = dc.PrivacyLevels.Where(a=>a.Name == Enum.GetName(typeof(Privacy.Levels), (int) level)).First();
            }
           
            Museum museum = new Museum()
            {
                Description = description,
                PrivacyLevelID = privacyLevel.ID,
                OwnerID = IdentityExtensions.GetUserId(modiByUser),
                ModiBy = IdentityExtensions.GetUserId(modiByUser),
                ModiDate = DateTime.Now
            };
            dc.Museums.InsertOnSubmit(museum);
            dc.SubmitChanges();
            return museum;
        }

        public PrivacyLevel createPrivacyLevel(String name, String description, String modiByUserID)
        {
            if (dc.AspNetUsers.Any(a => a.Id == modiByUserID))
            {
                PrivacyLevel level = new PrivacyLevel()
                {
                    Description = description,
                    Name = name,
                    ModiBy = modiByUserID,
                    ModiDate = DateTime.Now
                };
                dc.PrivacyLevels.InsertOnSubmit(level);
                dc.SubmitChanges();
                return level;
            }
            else
            {
                return null;
            }
            
        }
    }

   
}