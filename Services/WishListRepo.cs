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
	public class WishListRepo:IWishListRepo
	{
		private readonly IDbConnection _dbConnection;
		private readonly IConfiguration configuration;

		public WishListRepo(IDbConnection dbConnection, IConfiguration configuration)
		{
			_dbConnection = dbConnection;
			this.configuration = configuration;
		}

		public object AddToWishList(int BookId,int UserId)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("AddToWishList", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@BookId", BookId);
					cmd.Parameters.AddWithValue("@UserId", UserId);

					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return "Added To WishList";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<WishListModel> GetAllUserWishList(int UserId)
		{
			try
			{
				List<WishListModel> UserWishList = new List<WishListModel>();
				using (SqlCommand cmd = new SqlCommand("GetAllUserWishList", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@UserId", UserId);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							WishListModel Item = new WishListModel()
							{
								WishListId = (long)reader["WishListId"],
								UserId = (int)reader["UserId"],
								BookId = (int)reader["BookId"],
								BookName = reader["BookName"].ToString(),
								AuthorName = reader["AuthorName"].ToString(),
								Image = reader["Image"].ToString(),
								OriginalPrice = (int)reader["OriginalPrice"],
								DiscountRate = (short)reader["DiscountRate"],
								DiscountPrice = (double)reader["DiscountPrice"],
							};
							UserWishList.Add(Item);
						}
					}
					_dbConnection.Close();
					return UserWishList;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object DeleteWishListItem(long WishListId)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("DeleteWishListItem", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@WishListId", WishListId);

					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return "Item Deleted From WishList";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
