using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer_BS.Models;
using RepositoryLayer_BS.Interfaces;

namespace RepositoryLayer_BS.Services
{
	public class UserRepo:IUserRepo
	{
		private readonly IDbConnection _dbConnection;
		private readonly IConfiguration configuration;

		public UserRepo(IDbConnection dbConnection, IConfiguration configuration)
		{
			_dbConnection = dbConnection;
			this.configuration = configuration;
		}

		public SignUpModel SignUp(SignUpModel model)
        {
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("InsertUserData", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@FullName", model.FullName);
					cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
					cmd.Parameters.AddWithValue("@Password", model.Password);
					cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);

					cmd.ExecuteNonQuery();
					_dbConnection.Close();
					return model;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
        }

		public object Login(LoginModel model)
		{
			try
			{
				_dbConnection.Open();
				using (SqlCommand cmd = new SqlCommand("UserExists", (SqlConnection)_dbConnection))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
					cmd.Parameters.AddWithValue("Password", model.Password);

					var result = cmd.ExecuteScalar();
					_dbConnection.Close();
					if (result != null)
					{
						long userId = Convert.ToInt64(result);
						return GenerateToken(userId, model.EmailId);
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

		private string GenerateToken(long UserId, string EmailId)
		{
			// Create a symmetric security key using the JWT key specified in the configuration
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			// Create signing credentials using the security key and HMAC-SHA256 algorithm
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			// Define claims to be included in the JWT
			var claims = new[]
			{
				  new Claim("Email",EmailId),
				  new Claim("UserId", UserId.ToString())
			};
			// Create a JWT with specified issuer, audience, claims, expiration time, and signing credentials
			var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMonths(3),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
