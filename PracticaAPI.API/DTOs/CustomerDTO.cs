using System;
using System.Collections.Generic;

namespace PracticaAPI.API.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<CustomerOrderDTO> CustomerOrders { get; set; }
    }
}
