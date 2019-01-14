using System;
using System.Collections.Generic;

namespace PracticaAPI.API.DTOs
{ 
    public class ProductDTO
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual List<OrderDetailDTO> OrderDetail { get; set; }
    }
}
