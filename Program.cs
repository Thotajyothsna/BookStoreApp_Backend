using BussinessLayer_BS.Interfaces;
using BussinessLayer_BS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer_BS.Interfaces;
using RepositoryLayer_BS.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IDbConnection>(sp =>
	new SqlConnection(builder.Configuration.GetConnectionString("con")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserRepo,UserRepo>();
builder.Services.AddTransient<IUserBL,UserBL>();
builder.Services.AddTransient<IBooksRepo,BooksRepo>();
builder.Services.AddTransient<IBooksBL,BooksBL>();
builder.Services.AddTransient<ICartRepo, CartRepo>();
builder.Services.AddTransient<ICartBL,CartBL>();
builder.Services.AddTransient<IWishListRepo, WishListRepo>();
builder.Services.AddTransient<IWishListBL, WishListBL>();
builder.Services.AddTransient<IAddressRepo, AddressRepo>();
builder.Services.AddTransient<IAddressBL, AddressBL>();
builder.Services.AddTransient<IOrdersRepo,OrdersRepo>();
builder.Services.AddTransient<IOrdersBL,OrdersBL>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});
builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});

	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
