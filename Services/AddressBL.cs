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
	public class AddressBL:IAddressBL
	{
		private readonly IAddressRepo iAddressRepo;
		public AddressBL(IAddressRepo iAddressRepo)
		{
			this.iAddressRepo = iAddressRepo;
		}
		public object AddAddress(AddressModel address)
		{
			return iAddressRepo.AddAddress(address);
		}
		public object Updateaddress(AddressModel2 address)
		{
			return iAddressRepo.Updateaddress(address);
		}
		public object DeleteAddress(int AddressId)
		{
			return iAddressRepo.DeleteAddress(AddressId);
		}
		public List<AddressModel2> GetAllUserAddress(int UserId)
		{
			return iAddressRepo.GetAllUserAddress(UserId);
		}
	}
}
