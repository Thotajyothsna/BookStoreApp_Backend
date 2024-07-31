using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer_BS.Models;

namespace BussinessLayer_BS.Interfaces
{
	public interface IWishListBL
	{
		public object AddToWishList(int BookId, int UserId);
		public List<WishListModel> GetAllUserWishList(int UserId);
		public object DeleteWishListItem(long WishListId);
	}
}
