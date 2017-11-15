using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public interface IVehicleModelServis

    {
        Task<IEnumerable<Models.VehicleModel>> SelectAll(string sortOrder, string searchString, int pageNumber, int pageSize);
        Task<Models.VehicleModel> GetById(int id);
        Task<int> CreateAsync(Models.VehicleModel obj);
        Task<int> UpdateAsync(Models.VehicleModel obj);
        Task<int> DeleteAsync(int id);
    }
}
