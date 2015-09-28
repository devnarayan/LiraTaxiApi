using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Model
{
    public class StoreManagerModel
    {
        public int StoreManagerID { get; set; }
        public string RegistrationNo { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
