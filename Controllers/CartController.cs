using BussinessLayer_BS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartBL iCartBL;
		private readonly IConfiguration configuration;
        public CartController(ICartBL iCartBL, IConfiguration configuration)
        {
            this.configuration = configuration;
			this.iCartBL = iCartBL;
        }

		[HttpGet]
		[Authorize]
		[Route("AddToCart")]
		public IActionResult AddTocart(int BookId)
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			var result=iCartBL.AddToCart(BookId,UserId);
			if(result== "Added To Cart")
			{
				return Ok(new {status=true,msg=result});
			}
			else
			{
				return BadRequest(new { status=false,msg="Not Added To Cart"});
			}
		}

		[HttpPost]
		[Authorize]
		[Route("UpdateCart")]
		public IActionResult UpdateCart(int CartId,int Quantity)
		{
			var result=iCartBL.UpdateCart(CartId,Quantity);
			if(result== "Cart Updated")
			{
				return Ok(new { status = true, msg = result });
			}
			else
			{
				return BadRequest(new { status = false, msg = "Unable to Update Cart" });
			}
		}

		[HttpDelete]
		[Authorize]
		[Route("DeleteCart")]
		public IActionResult DeleteCart(int CartId)
		{
			var result= iCartBL.DeleteCart(CartId);
			if(result== "Item Deleted From Cart")
			{
				return Ok(new { status=true,msg=result });
			}
			else
			{
				return BadRequest(new { status = false, msg = "Unable to delete from cart" });
			}
		}

		[HttpGet]
		[Authorize]
		[Route("GetAllUserCart")]
		public IActionResult GetAllUsercart()
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			var result=iCartBL.GetAllUserCart(UserId);
			if (result.Count() > 0)
			{
				return Ok(new { status = true, msg = "Items present in your cart", data = result });
			}
			else if (result.Count() == 0)
			{
				return Ok(new { msg = "Nothing is added into cart" });
			}
			else
			{
				return BadRequest(new { msg = "Something Went Wrong Unable to fetch your cart items" });
			}
		}
	}
}
