using System;
using System.Collections.Generic;

namespace PracticaAPI.API.DTOs
{
    public class OrderDetailDTO
    {
        public int CustomerOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public virtual CustomerOrderDTO CustomerOrder { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
