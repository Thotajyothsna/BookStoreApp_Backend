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
	public class CartRepo:ICartRepo
	{
		private readonly IDbConnection _dbConnection;
		private readonly IConfiguration configuration;

		public CartRepo(IDbConnection dbConnection, IConfiguration configuration)
		{
			_dbConnection = dbConnection;
			this.configuration = configuration;
		}

		public object AddToCart(int BookId,int UserId)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("AddToCart", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@BookId", BookId);
					cmd.Parameters.AddWithValue("@UserId", UserId);
					
					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return "Added To Cart";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<CartModel> GetAllUserCart(int  UserId)
		{
			try
			{
				_dbConnection.Open();
				List<CartModel> UserCart = new List<CartModel>();
				using (SqlCommand cmd = new SqlCommand("GetAllUserCart", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@UserId",UserId);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							CartModel Item = new CartModel()
							{
								CartId = (long)reader["CartId"],
								UserId = (int)reader["UserId"],
								BookId = (int)reader["BookId"],
								Quantity = (int)reader["CartQuantity"],
								BookName = reader["BookName"].ToString(),
								AuthorName = reader["AuthorName"].ToString(),
								Image = reader["Image"].ToString(),
								OriginalPrice = (int)reader["OriginalPrice"],
								DiscountRate = (short)reader["DiscountRate"],
								DiscountPrice = (int)reader["DiscountPrice"],
								TotalPrice = (int)reader["TotalPrice"]
							};
							UserCart.Add(Item);
						}
					}
					_dbConnection.Close();
					return UserCart;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object UpdateCart(long CartId,int Quantity)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("UpdateCart", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@CartId", CartId);
					cmd.Parameters.AddWithValue("@Quantity", Quantity);

					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return "Cart Updated";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object DeleteCart(long CartId)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("DeleteCartItem", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@CartId", CartId);

					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return "Item Deleted From Cart";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
