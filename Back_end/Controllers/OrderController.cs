using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _db;
        private readonly ILogger<SizeController> _logger;

        public OrderController(OrderDbContext db, ILogger<SizeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(int id)
        {
            var orders = await _db.orderDetails.ToListAsync();
            return Ok(orders);
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _db.orders.Include(x => x.orderDetails).FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            // sent infor order dưới json(thường ứng dụng web or di động)
            return Ok(JsonConvert.SerializeObject(order, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }

        [HttpPost("CreateOrUpdateOrder")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreateOrUpdateOrder(OrderDetail orderDetail)
        {
            var ClientIdClaim = User.Claims.FirstOrDefault(x => x.Type == "UserId"); // get Id từ JWT in httpcontext
            if (ClientIdClaim == null || !int.TryParse(ClientIdClaim.Value, out int UserId))
            {
                return Unauthorized("Không thể xác thực người dùng. Vui lòng đăng nhập");
            }

            var product = await _db.products.FirstOrDefaultAsync(p => p.Id == orderDetail.ProductId); // check product in csdl
            if(product == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }
            if (orderDetail == null || orderDetail.Quantity <= 0 || orderDetail.Price <= 0 || orderDetail.ProductId <= 0)
            {
                return BadRequest("Thông tin sản phẩm không hợp lệ.");
            }
            if (product.Quantity < orderDetail.Quantity)
            {
                return BadRequest("Số lượng sản phẩm không đủ.");
            }
            var ExistingOrder = await _db.orders.Include(x => x.orderDetails).FirstOrDefaultAsync(x => x.UserId == UserId); // check order hiện có liên quan client
            // if not order
            if (ExistingOrder == null)
            {
                var newOrder = new Order
                {
                    OrderName = Request.Cookies["Username"], // get từ cookie user
                    Total = orderDetail.Price * orderDetail.Quantity,
                    OrderDate = DateTime.Now,
                    UserId = UserId,
                    orderDetails = new List<OrderDetail>() // chứa infor detail product in đơn hàng
                };
                newOrder.orderDetails.Add(new OrderDetail
                {
                    ProductId = orderDetail.ProductId,
                    Price = orderDetail.Price,
                    Quantity = orderDetail.Quantity,
                    Size = orderDetail.Size,
                    Total = orderDetail.Price * orderDetail.Quantity,
                });
                product.Quantity = product.Quantity - orderDetail.Quantity;
                _db.orders.Add(newOrder);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
            }
            else
            {
                var ExistingOrderDetail = ExistingOrder.orderDetails.FirstOrDefault(x => x.ProductId == orderDetail.ProductId && x.Size == orderDetail.Size);
                if (ExistingOrderDetail != null)
                {
                    ExistingOrderDetail.Quantity += orderDetail.Quantity;
                    ExistingOrderDetail.Price = orderDetail.Price;
                    product.Quantity -= orderDetail.Quantity; 
                }
                else
                {
                    // chưa có add sp ms vào list Orderdetail
                    ExistingOrder.orderDetails.Add(new OrderDetail
                    {
                        ProductId = orderDetail.ProductId, 
                        Price = orderDetail.Price,
                        Quantity = orderDetail.Quantity, 
                        Size = orderDetail.Size 
                    });
                    product.Quantity -= orderDetail.Quantity;
                }
            }
            ExistingOrder.Total += orderDetail.Price * orderDetail.Quantity;
            await _db.SaveChangesAsync();
            return Ok(ExistingOrder);
        }

    }
}
