using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITInventory.Models.Entities
{
    public class Device
    {
        [Key]
        public int DeviceID { get; set; }

        [Display(Name="Device Name")]
        public string DeviceName { get; set; }

        [Display(Name = "DeviceType")]
        public virtual int DeviceTypeFKID { get; set; }

        [Display(Name = "DeviceType")]
        [ForeignKey("DeviceTypeFKID")]
        public virtual DeviceType DeviceTypes { get; set; }

        public virtual int LocationFKID { get; set; }
        [Display(Name = "Location")]
        [ForeignKey("LocationFKID")]
        public Location Locations { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Entry Date")]
        public Nullable<DateTime> EntryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Assign Date")]
        public Nullable<DateTime> AssignDate { get; set; }

        [Display(Name = "Device Status")]
        public DeviceStatusEnum? DeviceStatus { get; set; }

        public string MACAddress { get; set; }

        public enum DeviceStatusEnum
        {
            Faulty,New,Old,UnderRepair,
        }
    }
}