using System;
using System.Collections.Generic;

namespace PracticaAPI.API.Models
{
    public partial class OrderDetailDTO
    {
        public int CustomerOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
