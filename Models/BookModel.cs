using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ModelLayer_BS.Models
{
	public class BookModel
	{
		public string BookName {  get; set; }
		public string AuthorName {  get; set; }
		public string Image {  get; set; }
		public string Description { get; set; }
		public int OriginalPrice {  get; set; }
		public int Quantity {  get; set; }
		public short DiscountRate {  get; set; }
		public short Rating {  get; set; }
		public int NoOfPeopleRated {  get; set; }
	}
}
