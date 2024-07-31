using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer_BS.Interfaces;
using ModelLayer_BS.Models;
using RepositoryLayer_BS.Interfaces;

namespace BussinessLayer_BS.Services
{
	public class UserBL:IUserBL
	{
		private readonly IUserRepo iuserRepo;
		public UserBL(IUserRepo iuserRepo)
		{
			this.iuserRepo = iuserRepo;
		}
		public SignUpModel SignUp(SignUpModel model)
		{
			return iuserRepo.SignUp(model);
		}
		public object Login(LoginModel model)
		{
			return iuserRepo.Login(model);
		}
	}
}
