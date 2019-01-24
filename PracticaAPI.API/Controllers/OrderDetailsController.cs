using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaAPI.API.Models;
using AutoMapper;
using PracticaAPI.API.DTOs;

namespace PracticaAPI.API.Controllers
{
    [Route("api/OrderDetails")]
    public class OrderDetailsController : Controller
    {
        private readonly StoreDBContext _context;

        public OrderDetailsController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public IEnumerable<OrderDetailDTO> GetOrderDetail()
        {
            return Mapper.Map<IEnumerable<OrderDetailDTO>>(_context.OrderDetails.OrderBy(x => x.Amount));
        }

        // GET: api/OrderDetails/Order/5
        [HttpGet("Order/{orderId}")]
        public IActionResult GetOrder_OrderDetail([FromRoute] int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderDetail = _context.OrderDetails
                .Include(x => x.Product)
                .Where(m => m.CustomerOrderId == orderId);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<IEnumerable<OrderDetailDTO>>(orderDetail));
        }

        // POST: api/OrderDetails
        [HttpPost]
        public async Task<IActionResult> PostOrderDetail([FromBody] List<OrderDetailDTO> orderDetail)
        {
            foreach (var item in orderDetail)
            {
                item.CustomerOrder = null;
                item.Product = null;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var od = Mapper.Map<IEnumerable<OrderDetail>>(orderDetail);
            _context.OrderDetails.AddRange(od);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetail", new { id = od.First().CustomerOrderId }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderDetail([FromRoute] int orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderDetail = _context.OrderDetails.Where(m => m.CustomerOrderId == orderId);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.RemoveRange(orderDetail);
            await _context.SaveChangesAsync();

            return Ok(orderDetail);
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.CustomerOrderId == id);
        }
    }
}