using BussinessLayer_BS.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer_BS.Models;

namespace BookStoreApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserBL iuserBL;
		private readonly IConfiguration configuration;
        public UserController(IUserBL iuserBL, IConfiguration configuration)
        {
            this.iuserBL = iuserBL;
			this.configuration = configuration;
        }

		[HttpPost]
		[Route("SignUp")]
		public IActionResult SignUp(SignUpModel model)
		{
			try
			{
				var Result = iuserBL.SignUp(model);
				if (Result !=null)
				{
					return Ok(new { status=true ,msg = "SignUp Successfull", data = Result });
				}
				else
				{
					return BadRequest(new {status=false, msg = "Unable to SignUp..Try again" });
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		[Route("Login")]
		public IActionResult Login(LoginModel model)
		{
			try
			{
				var Result = iuserBL.Login(model);
				if (Result != null)
				{
					return Ok(new { status=true ,msg = "Login Successfull", data = Result });
				}
				else
				{
					return BadRequest(new {status=false, msg = "Email or Password Is Incorrect" });
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
    }
}
