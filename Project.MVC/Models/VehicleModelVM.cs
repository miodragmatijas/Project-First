using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVC.Models
{
    public class VehicleModelVM
    {
        public int ID { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }

        public IEnumerable<SelectListItem> MakeLists { get; set; }
    }
}                                    