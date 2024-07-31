using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer_BS.Models;

namespace BussinessLayer_BS.Interfaces
{
	public interface IUserBL
	{
		public SignUpModel SignUp(SignUpModel model);
		public object Login(LoginModel model);
	}
}
