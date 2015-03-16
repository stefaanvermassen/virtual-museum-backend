using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VirtualMuseumAPI.Models
{
    public class MuseumModel
    {
        public int MuseumID { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime LastModified { get; set; }
    }
}