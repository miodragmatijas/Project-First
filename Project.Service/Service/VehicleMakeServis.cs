using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Project.Service.Models;
using PagedList;

namespace Project.Service.Service
{
    public class VehicleMakeServis : IVehicleMakeServis
    {
        VehicleContext.VehicleContext servisContext = new VehicleContext.VehicleContext();

        #region SelectAll
        public async Task<IEnumerable<VehicleMake>> SelectAll(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var VehiceMakeList = new List<VehicleMake>();

            var model = from s in servisContext.VehicleMakes select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    VehiceMakeList = await model.OrderByDescending(s => s.Name).ToListAsync();
                    break;
                case "abrv":
                    VehiceMakeList = await model.OrderBy(s => s.Abrv).ToListAsync();
                    break;
                case "abrv_desc":
                    VehiceMakeList = await model.OrderByDescending(s => s.Abrv).ToListAsync();
                    break;
                default:
                    VehiceMakeList = await model.OrderBy(s => s.Name).ToListAsync();
                    break;
            }          

            return VehiceMakeList.ToPagedList(pageNumber, pageSize);
        }
        #endregion

        #region GerById
        public async Task<VehicleMake> GetById(int id)
        {
            var query = from c in servisContext.VehicleMakes
                        where c.ID == id
                        select c;

            VehicleMake obj = await query.SingleOrDefaultAsync();
            return obj;
        }
        #endregion

        #region CreateAsync
        public Task<int> CreateAsync(VehicleMake obj)
        {
            servisContext.Entry(obj).State = EntityState.Added;
            return servisContext.SaveChangesAsync();
        }
        #endregion

        #region UpdateAsync
        public Task<int> UpdateAsync(VehicleMake obj)
        {
            servisContext.Entry(obj).State = EntityState.Modified;
            return servisContext.SaveChangesAsync();
        }
        #endregion

        #region DeleteAsync
        public Task<int> DeleteAsync(int id)
        {
            VehicleMake vehicleMakeDelete = new VehicleMake() { ID = id };

            servisContext.Entry(vehicleMakeDelete).State = EntityState.Deleted;
            return servisContext.SaveChangesAsync();
        }
        #endregion
    }
}
