using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VirtualMuseumAPI.Models
{
    public class ArtistModel
    {
        [Required]
        public int ArtistID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}