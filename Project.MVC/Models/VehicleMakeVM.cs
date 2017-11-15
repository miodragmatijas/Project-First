using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVC.Models
{
    public class VehicleMakeVM
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual ICollection<VehicleModel> VehicleModel { get; set; } 
    }
}