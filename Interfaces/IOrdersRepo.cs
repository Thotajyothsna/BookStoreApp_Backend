using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer_BS.Interfaces
{
	public interface IOrdersRepo
	{
		public object PlaceOrder(long CartId, long AddressId, int UserId);
		public object GetOrdersByUser(int UserId);
	}
}
