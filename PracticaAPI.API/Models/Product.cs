using System;
using System.Collections.Generic;

namespace PracticaAPI.API.Models
{
    public partial class ProductDTO
    {
        public ProductDTO()
        {
            OrderDetail = new HashSet<OrderDetailDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual ICollection<OrderDetailDTO> OrderDetail { get; set; }
    }
}
