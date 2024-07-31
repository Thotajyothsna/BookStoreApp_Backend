using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer_BS.Models;

namespace RepositoryLayer_BS.Interfaces
{
	public interface IAddressRepo
	{
		public object AddAddress(AddressModel address);
		public object Updateaddress(AddressModel2 address);
		public object DeleteAddress(int AddressId);
		public List<AddressModel2> GetAllUserAddress(int UserId);
	}
}
