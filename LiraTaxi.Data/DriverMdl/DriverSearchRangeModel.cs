using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.DriverMdl
{
    public class DriverSearchRangeModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public char Unit { get; set; } // M/K/N
        public string UserID { get; set; }
        public string Status { get; set; }
    }

}