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
	public class WishListBL:IWishListBL
	{
		private readonly IWishListRepo iWishListRepo;
        public WishListBL(IWishListRepo iWishListRepo)
        {
            this.iWishListRepo = iWishListRepo;
        }
		public object AddToWishList(int BookId, int UserId)
		{
			return iWishListRepo.AddToWishList(BookId, UserId);
		}
		public List<WishListModel> GetAllUserWishList(int UserId)
		{
			return iWishListRepo.GetAllUserWishList(UserId);
		}
		public object DeleteWishListItem(long WishListId)
		{
			return iWishListRepo.DeleteWishListItem(WishListId);
		}
	}
}
