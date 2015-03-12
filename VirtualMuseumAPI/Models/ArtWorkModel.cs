using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VirtualMuseumAPI.Models
{
    public class ArtWorkModel
    {
        
        public int ArtWorkID { get; set; }
        [Required]
        public int ArtistID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}