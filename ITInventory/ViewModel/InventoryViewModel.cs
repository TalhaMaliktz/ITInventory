using ITInventory.Models;
using ITInventory.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace ITInventory.ViewModel
{
    public class InventoryViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<DeviceHistory> myDeviceHistory { get; set; }
        public List<DeviceSpecifications> myDeviceSpecifications { get; set; }
        public List<DeviceToDevice> myDeviceToDevice { get; set; }
        public List<Device> myDevices { get; set; }
        public List<Type> myType { get; set; }
        public List<Location> myLocation { get; set; }
        public List<IP> myIP { get; set; }
        public List<SelectListItem> mySpecification { get; set; }

        public List<Device> GetDeviceData()
        {
            var myDevice = from dev in db.Device
                           select dev;
            return myDevice.ToList();
        }
        public List<DeviceSpecifications> GetDeviceSpecificationData()
        {
            var myDeviceSpecifications = from devspec in db.DeviceSpecifications
                                         select devspec;
            return myDeviceSpecifications.ToList();
        }

        public List<Location> GetLocationData()
        {
            var mylocations = from location in db.Locations
                              select location;
            return mylocations.ToList();
        }

        public List<DeviceToDevice> GetDeviceToDeviceData()
        {
            var mydeviceconnection = from deviceconnections in db.DeviceToDevices
                              select deviceconnections;
            return mydeviceconnection.ToList();
        }

    }
}