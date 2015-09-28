using LiraTaxi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiraTaxi.Data.Access.Auth
{
    public class CustomerService
    {
        public IEnumerable<CustomerModel> GetCustomers()
        {
            return null;
        }
        public CustomerModel GetCustomer(int id)
        {
            return null;
        }
        public string AddCustomer(CustomerModel customer)
        {
            return null;
        }
        public string EditCustomer(CustomerModel customer)
        {
            return "";
        }
        public string DeleteCustomer(int id)
        {
            return "";
        }
        public void Dispose()
        {

        }
    }
}
