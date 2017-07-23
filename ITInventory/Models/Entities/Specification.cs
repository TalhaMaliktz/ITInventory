using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITInventory.Models.Entities
{
    public class Specifications
    {
        [Key]
        public int SpecificationID { get; set; }

        [Display(Name = "Specification Name")]
        public string SpecificationValue { get; set; }
        public virtual int DeviceTypeFKID { get; set; }

        [ForeignKey("DeviceTypeFKID")]
        public virtual DeviceType DeviceTypes { get; set; }
    }
}