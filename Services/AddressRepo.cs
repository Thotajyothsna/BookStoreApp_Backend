using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelLayer_BS.Models;
using RepositoryLayer_BS.Interfaces;

namespace RepositoryLayer_BS.Services
{
	public class AddressRepo:IAddressRepo
	{
		private readonly IDbConnection _dbConnection;
		private readonly IConfiguration configuration;

		public AddressRepo(IDbConnection dbConnection, IConfiguration configuration)
		{
			_dbConnection = dbConnection;
			this.configuration = configuration;
		}

		public object AddAddress(AddressModel address)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("AddAddress", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@Address", address.Address);
					cmd.Parameters.AddWithValue("@City", address.City);
					cmd.Parameters.AddWithValue("@State",address.State);
					cmd.Parameters.AddWithValue("@AddressTypeId", address.AddressTypeId);
					cmd.Parameters.AddWithValue("@UserId", address.UserId);
					
					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return address;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object Updateaddress(AddressModel2 address)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("UpdateAddress", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@Address", address.Address);
					cmd.Parameters.AddWithValue("@City", address.City);
					cmd.Parameters.AddWithValue("@State", address.State);
					cmd.Parameters.AddWithValue("@AddressTypeId", address.AddressTypeId);
					cmd.Parameters.AddWithValue("@AddressId", address.AddressId);

					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return address;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object DeleteAddress(int AddressId) 
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("DeleteAddress", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@AddressId", AddressId);

					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return "Address Deleted";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<AddressModel2> GetAllUserAddress(int UserId)
		{
			try
			{
				List<AddressModel2> AllAddress = new List<AddressModel2>();
				using (SqlCommand cmd = new SqlCommand("GetAllUserAddress", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@UserId",UserId);
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							AddressModel2 address = new AddressModel2()
							{
								AddressId = (int)reader["AddressId"],
								Address = reader["Address"].ToString(),
								City = reader["City"].ToString(),
								State = reader["State"].ToString(),
								AddressTypeId = (int)reader["Quantity"],
								
							};
							AllAddress.Add(address);
						}
					}
					_dbConnection.Close();
					return AllAddress;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
