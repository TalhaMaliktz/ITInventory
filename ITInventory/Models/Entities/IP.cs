using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITInventory.Models.Entities
{
    public class IP
    {
        [Key]
        public int IPID { get; set; }
        public virtual int DeviceFKID { get; set; }

        [ForeignKey("DeviceFKID")]
        [Display(Name = "Device Name")]
        public virtual Device DeviceName { get; set; }
        public string FamilyIP { get; set; }
        public string ChildIP { get; set; }
        public string Purpose { get; set; }
    }
}