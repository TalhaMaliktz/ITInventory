using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITInventory.Models.Entities
{
    public class Location
    {
        public int LocationID { get; set; }

        [Display(Name = "Location")]
        public string LocationTypeValue { get; set; }

        [Display(Name = "Location Type")]
        public LocationTypeEnum? LocationType { get; set; }

        public enum LocationTypeEnum
        {
            Person,Room,Floor,Station,City
        }
    }
}