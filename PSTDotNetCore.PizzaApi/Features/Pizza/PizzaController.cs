using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSTDotNetCore.PizzaApi.Db;

namespace PSTDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AddDbContext _dbContext;

        public PizzaController()
        {
            _dbContext = new AddDbContext();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lst = await _dbContext.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtraAsync()
        {
            var lst = await _dbContext.PizzaExtras.ToListAsync();
            return Ok(lst);
        }


        [HttpGet("Order/{invoiceNo}")]
        public async Task<IActionResult> GetOrder(string invoiceNo)
        {
            var item = await _dbContext.PizzaOrders.FirstOrDefaultAsync(x => x.pizzaOrderInvoiceNo == invoiceNo);
            var lst = await _dbContext.PizzaOrderDetails.Where(x => x.pizzaOrderInvoiceNo == invoiceNo).ToListAsync();

            return Ok( new
            {
                Order = item,
                OrderDetail = lst
            });
        }


        [HttpPost("Order")]
        public async Task<IActionResult> GetOrderAsync(OrderRequest orderRequest)
        {
            var itemPizza = await _dbContext.Pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var total = itemPizza.Price;

            if(orderRequest.Extras.Length > 0)
            {
                var lstExtra = await _dbContext.PizzaExtras.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }

            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                pizzaId = orderRequest.PizzaId,
                pizzaOrderInvoiceNo = invoiceNo,
                TotalAmount = total
            };

            List<PizzaOrderDetailModel> pizzaExtraModels = orderRequest.Extras.Select(extraId => new PizzaOrderDetailModel()
            {
                PizzaExtraId = extraId,
                pizzaOrderInvoiceNo = invoiceNo

            }).ToList();

            await _dbContext.PizzaOrders.AddAsync(pizzaOrderModel);
            await _dbContext.PizzaOrderDetails.AddRangeAsync(pizzaExtraModels);
            await _dbContext.SaveChangesAsync();

            OrderResponse response = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thank you for your order! Enjoy your pizza!",
                TotalAmount = total
            };

            return Ok(orderRequest);
        }
    }
}
