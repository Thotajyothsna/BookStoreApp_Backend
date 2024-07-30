using BussinessLayer_BS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrdersBL iOrdersBL;
		private readonly IConfiguration configuration;
        public OrdersController(IOrdersBL iOrdersBL, IConfiguration configuration)
        {
            this.iOrdersBL = iOrdersBL;
			this.configuration = configuration;
        }

		[HttpGet]
		[Route("PlaceOrder")]
		[Authorize]
		public IActionResult PlaceOrder(long CartId, long AddressId)
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			var result=iOrdersBL.PlaceOrder(CartId,AddressId,UserId);
			if(result== "Order Placed Successfully")
			{
				return Ok(new { status = true, msg = "Order Placed Successfully" });
			}
			else
			{
				return BadRequest(new {status=false,msg="Unable to place order"});
			}
		}

		[HttpGet]
		[Route("UserOrders")]
		[Authorize]
		public IActionResult GetOrdersByUser()
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			var Result=iOrdersBL.GetOrdersByUser(UserId);
			if (Result!=null)
			{
				return Ok(new { status = true, msg = "List of Orders Placed", data = Result });
			}
			else
			{
				return BadRequest(new { status = false, msg = "Unable to fetch your orders" });
			}
		}
	}
}
