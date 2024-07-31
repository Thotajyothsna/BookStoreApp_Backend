using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer_BS.Models;

namespace BussinessLayer_BS.Interfaces
{
	public interface IBooksBL
	{
		public object AddBook(BookModel book);
		public List<BookModel3> GetAllBooks();
		public BookModel3 GetBookById(int id);
		public BookModel2 UpdateBook(BookModel2 book);
		public object DeleteBook(int BookId);
	}
}
