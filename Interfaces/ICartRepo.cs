using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer_BS.Models;

namespace RepositoryLayer_BS.Interfaces
{
	public interface ICartRepo
	{
		public object AddToCart(int BookId, int UserId);
		public List<CartModel> GetAllUserCart(int UserId);
		public object UpdateCart(long CartId, int Quantity);
		public object DeleteCart(long CartId);
	}
}
