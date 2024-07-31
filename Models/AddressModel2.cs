using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer_BS.Models
{
	public class AddressModel2
	{
		public int AddressId { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public int AddressTypeId { get; set; }
		public int UserId { get; set; }
	}
}
