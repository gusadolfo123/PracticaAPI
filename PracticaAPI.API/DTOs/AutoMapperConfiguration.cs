namespace PracticaAPI.API.DTOs
{
    using AutoMapper;
    using PracticaAPI.API.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDTO>()
                    .ForMember(c => c.CustomerOrders, o => o.Ignore())
                    .ReverseMap();
                
                cfg.CreateMap<CustomerOrder, CustomerOrderDTO>()
                    .ForMember(c => c.OrderDetails, o => o.Ignore())
                    .ReverseMap();

                cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();

                cfg.CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();

                cfg.CreateMap<OrderStatus, OrderStatusDTO>()
                    .ForMember(r => r.CustomerOrders, o => o.Ignore())
                    .ReverseMap();

                cfg.CreateMap<Product, ProductDTO>()
                    .ForMember(p => p.OrderDetails, o => o.Ignore())
                    .ReverseMap();
                    
            });
        }
    }
}
