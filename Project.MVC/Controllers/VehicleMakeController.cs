using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Service.Models;
using AutoMapper;
using Project.Service.VehicleContext;
using System.Threading.Tasks;
using PagedList;
using Project.MVC.Models;
using System.Net;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        #region Construct DI
        private readonly Service.Service.IVehicleMakeServis _vehicleServis;
        public VehicleMakeController(Service.Service.IVehicleMakeServis vehicleServis)
        {
            this._vehicleServis = vehicleServis;
        }
        #endregion

        #region GET: Index, Sorting, Searching, 
        public async Task<ActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            PaginationModel<VehicleMakeVM> paginationModel = new PaginationModel<VehicleMakeVM>
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

            var vehicleMakeList = await _vehicleServis.SelectAll(sortOrder, searchString, pageNumber, paginationModel.PAGE_SIZE);

            var model = Mapper.Map<List<Models.VehicleMakeVM>>(vehicleMakeList);

            paginationModel.OnePageModel = model;
            paginationModel.PageOnProductMake = vehicleMakeList;

            return View(paginationModel);
        }
        #endregion

        #region//GET Delete
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _vehicleServis.GetById(id);
            if (model == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(model);
        }
        #endregion
        #region//POST: Delete
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _vehicleServis.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region//GET Edit
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _vehicleServis.GetById(id);
            if (model == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(model);
        }
        #endregion
        #region//POST Edit
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            await _vehicleServis.UpdateAsync(vehicleMake);
            return RedirectToAction("Index");
        }
        #endregion

        #region// Add
        public ActionResult Add()
        {
            return View();
        }
        #endregion
        #region//POST: Add
        [HttpPost]
        public async Task<ActionResult> Add([Bind(Include = "Name,Abrv")] VehicleMake vehicleMake)
        {
            await _vehicleServis.CreateAsync(vehicleMake);

            return RedirectToAction("Index");
        }
        #endregion
    }
}