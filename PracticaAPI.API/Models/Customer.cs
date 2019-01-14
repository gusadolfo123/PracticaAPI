using System;
using System.Collections.Generic;

namespace PracticaAPI.API.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerOrders = new HashSet<CustomerOrder>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}
