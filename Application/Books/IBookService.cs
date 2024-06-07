using Application.Authors.Requests;
using Application.Books.Model;
using Application.Books.Requests;
using Application.Books.Responses;
using Application.Shared;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books
{
    public interface IBookService
    {
        Task<Response<string>> CreateBook(AddBookRequest request);
        Task<Response<DeleteBookResponse>> DeleteBook(string id);
        Task<Response<IEnumerable<GetBookModel>>> GetAllBooks();
        Task<Response<Book>> GetBookById(string id);
    }
}
