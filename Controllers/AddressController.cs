using BussinessLayer_BS.Interfaces;
using BussinessLayer_BS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer_BS.Models;

namespace BookStoreApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AddressController : ControllerBase
	{
		private readonly IAddressBL iAddressBL;
		private readonly IConfiguration configuration;
        public AddressController(IAddressBL iAddressBL, IConfiguration configuration)
        {
            this.configuration = configuration;
			this.iAddressBL = iAddressBL;
        }

		[HttpPost]
		[Route("addAddress")]
		[Authorize]
		public IActionResult AddAddress(AddressModel addressModel)
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			addressModel.UserId = UserId;
			var Result=iAddressBL.AddAddress(addressModel);
			if (Result != null)
			{
				return Ok(Result);
			}
			else
			{
				return BadRequest(new { status = false, msg = "Unable to add address" });
			}
		}

		[HttpPost]
		[Route("UpdateAddress")]
		[Authorize]
		public IActionResult UpdateAddress(AddressModel2 addressModel)
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			addressModel.UserId = UserId;
			var Result=iAddressBL.Updateaddress(addressModel);
			if (Result != null)
			{
				return Ok(Result);
			}
			else
			{
				return BadRequest(new { status = true, msg = "Unable to Update Address" });
			}
		}

		[HttpDelete]
		[Route("DeleteAddress")]
		[Authorize]
		public IActionResult DeleteAddress(int AddressId)
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			var Result=iAddressBL.DeleteAddress(AddressId);
			if (Result== "Address Deleted")
			{
				return Ok(new { msg = "Deleted" });
			}
			else
			{
				return BadRequest(new { status = false, msg = "Unable to delete" });
			}
		}

		[HttpGet]
		[Authorize]
		[Route("GetAllUserAddress")]
		public IActionResult GetAllUserAddress()
		{
			int UserId = Convert.ToInt32(User.FindFirst(x => x.Type == "UserId").Value);
			var result = iAddressBL.GetAllUserAddress(UserId);
			if (result.Count() > 0)
			{
				return Ok(new { status = true, msg = "Added addresses", data = result });
			}
			else if (result.Count() == 0)
			{
				return Ok(new { msg = "You didn't add your address.Please add it" });
			}
			else
			{
				return BadRequest(new { msg = "Something Went Wrong Unable to fetch Your Addresses" });
			}
		}
	}
}
