using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class VehicleModel
    {
        public int ID { get; set; }

        [ForeignKey("VehicleMake")]
        public virtual int MakeId { get; set; }

        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
    }
}
