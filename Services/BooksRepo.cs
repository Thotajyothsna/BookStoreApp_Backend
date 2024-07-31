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
	public class BooksRepo: IBooksRepo
	{
		private readonly IDbConnection _dbConnection;
		private readonly IConfiguration configuration;

		public BooksRepo(IDbConnection dbConnection, IConfiguration configuration)
		{
			_dbConnection = dbConnection;
			this.configuration = configuration;
		}

		public object AddBook(BookModel book)
		{
			try
			{
				_dbConnection.Open();
				using(SqlCommand cmd = new SqlCommand("AddBook", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@BookName", book.BookName);
					cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
					cmd.Parameters.AddWithValue("@Image", book.Image);
					cmd.Parameters.AddWithValue("@Description",book.Description);
					cmd.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
					cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
					cmd.Parameters.AddWithValue("@DiscountRate", book.DiscountRate);
					
					var result = cmd.ExecuteScalar();
					_dbConnection.Close();

					if (result != null && Convert.ToInt32(result) == 1)
					{
						return book;
					}
					else
					{
						return null;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<BookModel3> GetAllBooks()
		{
			try
			{
				_dbConnection.Open();
				List<BookModel3> Books = new List<BookModel3>();
				using(SqlCommand cmd = new SqlCommand("GetAllBooks", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							BookModel3 book = new BookModel3()
							{
								BookId = (int)reader["BookId"],
								BookName = reader["BookName"].ToString(),
								AuthorName = reader["AuthorName"].ToString(),
								Image = reader["Image"].ToString(),
								Description = reader["Description"].ToString(),
								OriginalPrice = (int)reader["OriginalPrice"],
								Quantity = (int)reader["Quantity"],
								DiscountRate = (short)reader["DiscountRate"],
								DiscountPrice = (double)reader["DiscountPrice"],
								Rating = (short)reader["Rating"],
								NoOfPeopleRated = (int)reader["NoOfPeopleRated"]
							};
							Books.Add(book);
						}
					}
					_dbConnection.Close();
					return Books;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public BookModel3 GetBookById(int id) 
		{
			try
			{
				_dbConnection.Open();
				BookModel3 book=new BookModel3();
				using (SqlCommand cmd = new SqlCommand("GetbyId", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("BookId", id);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							book=new BookModel3()
							{
								BookId = (int)reader["BookId"],
								BookName = reader["BookName"].ToString(),
								AuthorName = reader["AuthorName"].ToString(),
								Image = reader["Image"].ToString(),
								Description = reader["Description"].ToString(),
								OriginalPrice = (int)reader["OriginalPrice"],
								Quantity = (int)reader["Quantity"],
								DiscountRate = (short)reader["DiscountRate"],
								DiscountPrice = (double)reader["DiscountPrice"],
								Rating = (short)reader["Rating"],
								NoOfPeopleRated = (int)reader["NoOfPeopleRated"]
							};
						}
					}
					_dbConnection.Close();
					return book;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public BookModel2 UpdateBook(BookModel2 book)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("UpdateBook", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@BookId", book.BookId);
					cmd.Parameters.AddWithValue("@BookName", book.BookName);
					cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
					cmd.Parameters.AddWithValue("@Image", book.Image);
					cmd.Parameters.AddWithValue("@Description",book.Description);
					cmd.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
					cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
					cmd.Parameters.AddWithValue("@DiscountRate", book.DiscountRate);
					
					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return book;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object DeleteBook(int  BookId)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("DeleteBook", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@BookId", BookId);
					
					cmd.ExecuteNonQuery();

					_dbConnection.Close();
					return "Deleted";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
