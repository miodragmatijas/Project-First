using System;
using Project.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AutoMapper;
using System.Threading.Tasks;
using Project.MVC.Models;
using System.Net;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        #region Construct DI
        private readonly Service.Service.IVehicleModelServis _vehicleServis;
        private readonly Service.Service.IVehicleMakeServis _vehicleMakeServis;

        public VehicleModelController(Service.Service.IVehicleModelServis vehicleServis, Service.Service.IVehicleMakeServis vehicleMakeServis)
        {
            this._vehicleServis = vehicleServis;
            this._vehicleMakeServis = vehicleMakeServis;
        }

        #endregion

        #region // GET: Index -- dodati find by Make
        public async Task<ActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page, string searchStringMake)
        {
            PaginationModel<VehicleModelVM> paginationModel = new PaginationModel<VehicleModelVM>
            {
                SortName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "",
                SortAbrv = sortOrder == "abrv" ? "abrv_desc" : "abrv"
            };

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            paginationModel.CurrentFilter = searchString;
            paginationModel.CurrentSort = sortOrder;

            int pageNumber = (page ?? 1);

            var vehicleModelList = await _vehicleServis.SelectAll(sortOrder, searchString, pageNumber, paginationModel.PAGE_SIZE);

            var model = Mapper.Map<List<Models.VehicleModelVM>>(vehicleModelList);

            paginationModel.OnePageModel= model;
            paginationModel.PageOnProductModel = vehicleModelList;

            return View(paginationModel);
        }
        #endregion

        #region // GET Add
        public async Task<ActionResult> Add()
        {
            var vehicleMakeList = await _vehicleMakeServis.SelectAll("Name", "", 1, 10000);

            SelectList list = new SelectList(vehicleMakeList, "ID", "Name");

            VehicleModelVM vehicleModelVM = new VehicleModelVM();

            vehicleModelVM.MakeLists = list;


            return View(vehicleModelVM);
        }
        #endregion
        #region // POST Add
        [HttpPost]
        public async Task<ActionResult> Add([Bind(Include = "ID,Name,MakeId,Abrv")] VehicleModel vehicleModel)
        {
            await _vehicleServis.CreateAsync(vehicleModel);            

            return RedirectToAction("Index");

        }
        #endregion

        #region // GET Edit
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var vehicleMakeList = await _vehicleMakeServis.SelectAll("Name", "", 1, 10000);

            SelectList list = new SelectList(vehicleMakeList, "ID", "Name");
            ViewBag.MakeLists = list;

            var model = await _vehicleServis.GetById(id);
            if (model == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            
            return View(model);
        }
        #endregion
        #region // POST Edit
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,MakeId,Abrv")] VehicleModel vehiclmeModel)
        {
            await _vehicleServis.UpdateAsync(vehiclmeModel);

            return RedirectToAction("Index");
        }
        #endregion 

        #region // GET Delete
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _vehicleServis.GetById(id);
            if (model == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(model);
        }
        #endregion
        #region // POST Delete
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            await _vehicleServis.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
