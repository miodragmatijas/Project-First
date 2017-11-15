using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public interface IVehicleMakeServis
    {
        Task<IEnumerable<Models.VehicleMake>> SelectAll(string sortOrder,string searchString, int pageNumber, int pageSize);
        Task<Models.VehicleMake> GetById(int id);
        Task<int> CreateAsync(Models.VehicleMake obj);
        Task<int> UpdateAsync(Models.VehicleMake obj);
        Task<int> DeleteAsync(int id);
    }

}