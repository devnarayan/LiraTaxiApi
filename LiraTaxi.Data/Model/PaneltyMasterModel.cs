using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Model
{
   public class PaneltyMasterModel
    {
        public int PaneltyMasterID { get; set; }
        public string PaneltyType { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Description { get; set; }
    }
}
