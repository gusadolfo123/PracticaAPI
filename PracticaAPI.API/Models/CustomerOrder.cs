using System;
using System.Collections.Generic;

namespace PracticaAPI.API.Models
{
    public partial class CustomerOrder
    {
        public CustomerOrder()
        {
            OrderDetail = new HashSet<OrderDetailDTO>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public int OrderStatusId { get; set; }
        public decimal Amount { get; set; }

        public virtual CustomerDTO Customer { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetail { get; set; }
    }
}
