using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITInventory.Models.Entities
{
    public class DeviceType
    {
        [Key]
        [Display(Name = "Device Type")]
        public int DeviceTypeID { get; set; }
        [Display(Name = "Type")]
        public string DeviceTypeValue { get; set; }
    }
}