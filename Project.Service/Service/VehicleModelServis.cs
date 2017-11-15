using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Project.Service.Models;
using PagedList;

namespace Project.Service.Service
{
    public class VehicleModelServis : IVehicleModelServis
    {
        VehicleContext.VehicleContext servisContext = new VehicleContext.VehicleContext();

        #region SelectAll
        public async Task<IEnumerable<VehicleModel>> SelectAll(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var VehiceModelList = new List<VehicleModel>();

            var model = from s in servisContext.VehicleModels select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    VehiceModelList = await model.OrderByDescending(s => s.Name).ToListAsync();
                    break;
                case "abrv":
                    VehiceModelList = await model.OrderBy(s => s.Abrv).ToListAsync();
                    break;
                case "abrv_desc":
                    VehiceModelList = await model.OrderByDescending(s => s.Abrv).ToListAsync();
                    break;
                default:
                    VehiceModelList = await model.OrderBy(s => s.Name).ToListAsync();
                    break;
            }           

            return VehiceModelList.ToPagedList(pageNumber, pageSize);
        }
        #endregion

        #region GetById
        public async Task<VehicleModel> GetById(int id)
        {
            var query = from c in servisContext.VehicleModels
                        where c.ID == id
                        select c;

            VehicleModel obj = await query.SingleOrDefaultAsync();
            return obj;
        }

        public async Task<int> CreateAsync(VehicleModel obj)
        {
            var x = from c in servisContext.VehicleMakes where c.ID == obj.MakeId select c.ID;

            if (x.Any())
            {
                servisContext.Entry(obj).State = EntityState.Added;
                await servisContext.SaveChangesAsync();
                return await Task.FromResult(1);
            }
            else
            {
                return await Task.FromResult(0);
            }
        }
        #endregion

        #region UpdateAsync
        public Task<int> UpdateAsync(VehicleModel obj)
        {
            servisContext.Entry(obj).State = EntityState.Modified;
            return servisContext.SaveChangesAsync();
        }
        #endregion

        #region DeleteAsync
        public Task<int> DeleteAsync(int id)
        {
            VehicleModel vehicleMakeDelete = new VehicleModel() { ID = id };

            servisContext.Entry(vehicleMakeDelete).State = EntityState.Deleted;
            return servisContext.SaveChangesAsync();
        }
        #endregion
    }
}
