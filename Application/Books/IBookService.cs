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
        Task<Response<UpdateBookResponse>> UpdateBook(UpdateBookRequest request);
        Task<Response<DeleteBookResponse>> DeleteBook(string id);
        Task<Response<IEnumerable<BookModel>>> GetAllBooks();
        Task<Response<BookModel>> GetBookById(string id);
        Task<Response<ChangeBookStatusResponse>> ChangeBookStatus(string id);
    }
}
