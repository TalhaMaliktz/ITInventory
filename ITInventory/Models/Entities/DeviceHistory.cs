using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITInventory.Models.Entities
{
    public class DeviceHistory
    {
        [Key]
        public int DeviceHistoryID { get; set; }
        public int DeviceFKID { get; set; }
        [ForeignKey("DeviceFKID")]
        public Device DeviceName { get; set; }
        [Display(Name = "Date Repaired")]
        public DateTime DateRepaired { get; set; }
        public string Remarks { get; set; }
    }
}