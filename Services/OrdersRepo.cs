using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelLayer_BS.Models;
using RepositoryLayer_BS.Interfaces;

namespace RepositoryLayer_BS.Services
{
	public class OrdersRepo:IOrdersRepo
	{
		private readonly IDbConnection _dbConnection;
		private readonly IConfiguration configuration;

		public OrdersRepo(IDbConnection dbConnection, IConfiguration configuration)
		{
			_dbConnection = dbConnection;
			this.configuration = configuration;
		}

		public object PlaceOrder(long CartId,long AddressId,int UserId)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("PlaceOrder", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@CartId", CartId);
					cmd.Parameters.AddWithValue("@UserId", UserId);
					cmd.Parameters.AddWithValue("@AddressId",AddressId);

					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return "Order Placed Successfully";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object GetOrdersByUser(int  UserId)
		{
			try
			{
				_dbConnection.Open();
				List<OrdersModel> UserOrders = new List<OrdersModel>();
				using (SqlCommand cmd = new SqlCommand("GetOrdersByUser", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@UserId", UserId);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							OrdersModel Order = new OrdersModel()
							{
								CartId = (long)reader["CartId"],
								UserId = (int)reader["UserId"],
								BookId = (int)reader["BookId"],
								Quantity = (int)reader["CartQuantity"],
								BookName = reader["BookName"].ToString(),
								AuthorName = reader["AuthorName"].ToString(),
								Image = reader["Image"].ToString(),
								TotalOriginalPrice = (int)reader["TotalOriginalPrice"],
								DiscountRate = (short)reader["DiscountRate"],
								TotalDiscountPrice = (int)reader["TotalDiscountPrice"]
								
							};
							UserOrders.Add(Order);
						}
					}
					_dbConnection.Close();
					return UserOrders;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
