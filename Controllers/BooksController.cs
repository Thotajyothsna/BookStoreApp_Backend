using BussinessLayer_BS.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer_BS.Models;

namespace BookStoreApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBooksBL iBooksBL;
		private readonly IConfiguration configuration;
		public BooksController(IBooksBL iBooksBL, IConfiguration configuration) 
		{
			this.iBooksBL = iBooksBL;
			this.configuration = configuration;
		}

		[HttpPost]
		[Route("AddBook")]
		public IActionResult AddBook(BookModel book)
		{
			var result=iBooksBL.AddBook(book);
			if (result != null)
			{
				return Ok(new { status = true, msg = "Book Successfully Added" });
			}
			else
			{
				return BadRequest(new {status=false,msg="Unable to add Book"});
			}
		}

		[HttpPost]
		[Route("EditBook")]
		public IActionResult EditBook(BookModel2 book)
		{
			var result = iBooksBL.UpdateBook(book);
			if (result != null)
			{
				return Ok(new { status = true, msg = "Book Updated Successfully", data = result });
			}
			else
			{
				return BadRequest(new {status=false,msg="Unable to Update"});
			}
		}
		[HttpGet]
		[Route("GetAllBooks")]
		public IActionResult GetAllBooks()
		{
			var result=iBooksBL.GetAllBooks();
			if (result.Count() > 0)
			{
				return Ok(new { status = true, msg = "Avialable Books", data = result });
			}
			else if (result.Count() == 0)
			{
				return Ok(new { msg = "No Book is Available" });
			}
			else
			{
				return BadRequest(new { msg = "Something Went Wrong Unable to fetch Available Books" });
			}
		}

		[HttpGet]
		[Route("DeleteBook/{BookId}")]
		public IActionResult DeleteBook(int BookId)
		{
			var result=iBooksBL.DeleteBook(BookId);
			if (result == "Deleted")
			{
				return Ok(new { status = true, msg = "Book deleted Successfully" });
			}
			else
			{
				return BadRequest(new { status = false, msg = "Unable to delete Book" });
			}
		}

		[HttpGet]
		[Route("GetById/{BookId}")]
		public IActionResult GetBookById(int BookId)
		{
			var result=iBooksBL.GetBookById(BookId);
			if(result != null)
			{
				return Ok(result);
			}
			else { return NotFound(); }
		}
	}
}
