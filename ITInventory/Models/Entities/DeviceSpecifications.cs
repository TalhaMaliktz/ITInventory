using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITInventory.Models.Entities
{
    public class DeviceSpecifications
    {
        [Key]
        public int DeviceSpecificationID { get; set; }
        [Display(Name = "Device Name")]
        public virtual int DeviceFKID { get; set; }
        [ForeignKey("DeviceFKID")]
        public Device Device { get; set; }
        public virtual int SpecificationFKID { get; set; }
        [ForeignKey("SpecificationFKID")]
        public Specifications SpecificationTitle { get; set; }

        public string SpecificationValue { get; set; }
        [Display(Name = "DeviceType")]
        public virtual int DeviceTypeFKID { get; set; }

        [Display(Name = "DeviceType")]
        [ForeignKey("DeviceTypeFKID")]
        public virtual DeviceType DeviceTypes { get; set; }
    }
}