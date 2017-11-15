using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Service.VehicleContext
{   

    public class VehicleContext : DbContext
    {
        
        public VehicleContext() : base("Vehicle")
        {
            Database.SetInitializer<VehicleContext>(new CreateDatabaseIfNotExists<VehicleContext>());
        }

        public DbSet<VehicleMake > VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        
    }
}
