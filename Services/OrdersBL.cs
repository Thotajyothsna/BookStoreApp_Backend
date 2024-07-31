using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer_BS.Interfaces;
using RepositoryLayer_BS.Interfaces;

namespace BussinessLayer_BS.Services
{
	public class OrdersBL:IOrdersBL
	{
		private readonly IOrdersRepo iOrdersRepo;
        public OrdersBL(IOrdersRepo iOrdersRepo)
        {
            this.iOrdersRepo = iOrdersRepo;
        }
		public object PlaceOrder(long CartId, long AddressId, int UserId)
		{
			return iOrdersRepo.PlaceOrder(CartId, AddressId, UserId);
		}
		public object GetOrdersByUser(int UserId)
		{
			return iOrdersRepo.GetOrdersByUser(UserId);
		}
	}
}
