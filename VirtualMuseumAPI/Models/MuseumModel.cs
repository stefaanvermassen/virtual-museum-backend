﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VirtualMuseumAPI.Helpers;

namespace VirtualMuseumAPI.Models
{
    /// <summary>
    /// A Virtual Museum Object
    /// </summary>
    public class MuseumModel
    {
        /// <summary>
        /// The Museum's unique ID.
        /// </summary>
        public int MuseumID { get; set; }
        /// <summary>
        /// The museum's description.
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// When is the museum modified.
        /// </summary>
        public DateTime LastModified { get; set; }
        /// <summary>
        /// The privacy level of the museum, make sure to use the right int values. 
        /// This can be done by using the same enum in the client app.
        /// </summary>
        [Required]
        public Privacy.Levels Privacy { get; set; }
    }
}