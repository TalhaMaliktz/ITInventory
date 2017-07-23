using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITInventory.Models.Entities
{
    public class DeviceToDevice
    {
        [Key]
        public int DeviceToDeviceID { get; set; }
        public virtual int DeviceFKID { get; set; }

        [ForeignKey("DeviceFKID")]
        public Device Devices { get; set; }
        public virtual int DeviceFKIDConnected { get; set; }

        [ForeignKey("DeviceFKIDConnected")]
        public Device DeviceConnected { get; set; }
    }
}