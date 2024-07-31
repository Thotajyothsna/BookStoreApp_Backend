using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer_BS.Interfaces
{
	public interface IOrdersBL
	{
		public object PlaceOrder(long CartId, long AddressId, int UserId);
		public object GetOrdersByUser(int UserId);
	}
}
