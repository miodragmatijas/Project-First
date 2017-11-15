using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVC.Models
{
    public class PaginationModel<T>
    {
        public string SortName { get; set; }
        public string SortAbrv { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentFilter { get; set; }

        public IEnumerable<VehicleModel> PageOnProductModel { get; set; }
        public  IEnumerable<VehicleMake> PageOnProductMake { get; set; }

        public IEnumerable<T> OnePageModel { get; set; }

        public  int PAGE_SIZE = 5;
    }
}