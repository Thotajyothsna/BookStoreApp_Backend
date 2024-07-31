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
	public class CartBL:ICartBL
	{
		private readonly ICartRepo iCartRepo;
        public CartBL(ICartRepo iCartRepo)
        {
            this.iCartRepo = iCartRepo;
        }
		public object AddToCart(int BookId, int UserId)
		{
			return iCartRepo.AddToCart(BookId, UserId);
		}
		public List<CartModel> GetAllUserCart(int UserId)
		{
			return iCartRepo.GetAllUserCart(UserId);
		}
		public object UpdateCart(long CartId, int Quantity)
		{
			return iCartRepo.UpdateCart(CartId, Quantity);
		}
		public object DeleteCart(long CartId)
		{
			return iCartRepo.DeleteCart(CartId);
		}
	}
}
