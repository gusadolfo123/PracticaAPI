using System;
using System.Collections.Generic;

namespace PracticaAPI.API.DTOs
{
    public class OrderStatusDTO
    {       
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CustomerOrderDTO> CustomerOrder { get; set; }
    }
}
