using Application.Authors.Models;
using Application.Authors.Requests;
using Application.Authors.Responses;
using Application.Books.Model;
using Application.Books.Requests;
using Application.Books.Responses;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Application.Shared.ReponseExtensions;

namespace Application.Books
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(
            IBookRepository bookRepository, 
            IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        public async Task<Response<string>> CreateBook(AddBookRequest request)
        {
            var book = request.Adapt<Book>();

            var authors = new List<AuthorBooks>();

            foreach (var id in request.AuthorIds)
            {
                var author = await _authorRepository.GetById(id);
                if(author == null)
                {
                    return Fail<string>(ErrorCode.NotFound, "No Author was found with the provided Id");
                }
                authors.Add(new AuthorBooks() { AuthorId = id });
            }

            book.AuthorBooks = authors;

            if (request.Image.FileName == null || request.Image.FileName.Length == 0)
            {
                throw new FileLoadException("File not selected");
            }
            var path = Path.Combine(Environment.CurrentDirectory, request.Image.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
                stream.Close();
            }

            book.ImagePath = path;

            var result = await _bookRepository.Add(book);
            return Ok(result);
        }

        public async Task<Response<DeleteBookResponse>> DeleteBook(string id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
            {
                return Fail<DeleteBookResponse>(ErrorCode.NotFound, "No Book was found with the provided Id");
            }

            await _bookRepository.Delete(book);
            return Ok<DeleteBookResponse>();
        }

        public async Task<Response<IEnumerable<GetBookModel>>> GetAllBooks()
        {
            var books = await _bookRepository.GetBooksWithAuthors();
            var result = books.Adapt<IEnumerable<GetBookModel>>();
            return Ok(result);
        }

        public async Task<Response<Book>> GetBookById(string id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
            {
                return Fail<Book>(ErrorCode.NotFound, "No Book was found with the provided Id");
            }

            return Ok(book);
        }
    }
}
