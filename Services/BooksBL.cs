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
	public class BooksBL: IBooksBL
	{
		private readonly IBooksRepo iBookRepo;
        public BooksBL(IBooksRepo iBookRepo)
        {
            this.iBookRepo = iBookRepo;
        }

		public object AddBook(BookModel book)
		{
			return iBookRepo.AddBook(book);
		}
		public List<BookModel3> GetAllBooks()
		{
			return iBookRepo.GetAllBooks();
		}
		public BookModel3 GetBookById(int id)
		{
			return iBookRepo.GetBookById(id);
		}
		public BookModel2 UpdateBook(BookModel2 book)
		{
			return iBookRepo.UpdateBook(book);
		}
		public object DeleteBook(int BookId)
		{
			return iBookRepo.DeleteBook(BookId);
		}
	}
}
