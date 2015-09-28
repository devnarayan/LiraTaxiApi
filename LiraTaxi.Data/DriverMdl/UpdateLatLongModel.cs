using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.DriverMdl
{
    public class UpdateLatLongModel
    {
        public string UserId { get; set; }
        public string UserType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string UpdateTime { get; set; }
    }
}
