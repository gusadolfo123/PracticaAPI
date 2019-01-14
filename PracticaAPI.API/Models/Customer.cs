using System;
using System.Collections.Generic;

namespace PracticaAPI.API.Models
{
    public partial class CustomerDTO
    {
        public CustomerDTO()
        {
            CustomerOrder = new HashSet<CustomerOrder>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
    }
}
