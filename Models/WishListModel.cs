using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer_BS.Models
{
	public class WishListModel
	{
		public long WishListId { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
		public string BookName { get; set; }
		public string AuthorName { get; set; }
		public string Image { get; set; }
		public int OriginalPrice { get; set; }
		public short DiscountRate { get; set; }
		public double DiscountPrice { get; set; }
	}
}
