using System.Diagnostics.Eventing.Reader;
using BussinessLayer_BS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WishListController : ControllerBase
	{
		private readonly IWishListBL iWishListBL;
		private readonly IConfiguration configuration;
		public WishListController(IConfiguration configuration, IWishListBL iWishListBL)
		{
			this.configuration = configuration;
			this.iWishListBL = iWishListBL;
		}

		[HttpGet]
		[Route("AddToWishList")]
		[Authorize]
		public IActionResult AddToWishList(int BookId)
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			var result=iWishListBL.AddToWishList(BookId, UserId);
			if(result!=null)
			{
				return Ok(new {status=true,msg="Items in WishList",data=result}); 
			}
			else
			{
				return BadRequest(new { status = false, msg = "Unable to fetch" });
			}
		}

		[HttpGet]
		[Authorize]
		[Route("UserWishList")]
		public IActionResult GetAllUserWishList()
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			var result = iWishListBL.GetAllUserWishList(UserId);
			if (result.Count() > 0)
			{
				return Ok(new { status = true, msg = "Items in WishList", data = result });
			}
			else if(result.Count()== 0)
			{
				return Ok(new { msg = "No item is added into WishList..Add Something you like" });
			}
			else 
			{
				return BadRequest(new { msg = "Something Went Wrong Unable to fetch Your WishList" });
			}
		}

		[HttpDelete]
		[Authorize]
		[Route("DeleteWishListItem")]
		public IActionResult DeleteWishListItem(long WishListId)
		{
			var result=iWishListBL.DeleteWishListItem(WishListId);
			if(result== "Item Deleted From WishList")
			{
				return Ok(new { status = true,msg=result});
			}
			else
			{
				return NotFound();
			}
		}
	}
}