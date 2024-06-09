using Application.Auth.Requests;
using Application.Authors.Models;
using Application.Authors.Requests;
using Application.Authors.Responses;
using Application.Books.Model;
using Application.Books.Requests;
using Application.Books.Responses;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
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
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly IConfiguration _configuration;

        public BookService(
            IBookRepository bookRepository, 
            IAuthorRepository authorRepository,
            IAuthorBookRepository authorBookRepository,
            IConfiguration configuration)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorBookRepository = authorBookRepository;
            _configuration = configuration;
        }
        public async Task<Response<string>> CreateBook(AddBookRequest request)
        {
            AddBookRequestValidator validator = new AddBookRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Fail<string>(ErrorCode.ValidationFailed, validationResult);
            }

            var book = request.Adapt<Book>();

            var authors = new List<AuthorBooks>();

            foreach (var id in request.AuthorIds)
            {
                var author = await _authorRepository.GetById(id);
                if(author == null)
                {
                    return Fail<string>(ErrorCode.AuthorNotFound, "No Author was found with the provided Id");
                }
                authors.Add(new AuthorBooks() { AuthorId = id });
            }

            book.AuthorBooks = authors;

            var path = Path.Combine(_configuration["ImagesPath"], request.Image.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
                stream.Close();
            }

            book.ImagePath = request.Image.FileName;

            var result = await _bookRepository.Add(book);
            return Ok(result);
        }

        public async Task<Response<UpdateBookResponse>> UpdateBook(UpdateBookRequest request)
        {
            UpdateBookRequestValidator validator = new UpdateBookRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Fail<UpdateBookResponse>(ErrorCode.ValidationFailed, validationResult);
            }

            var book = await _bookRepository.GetById(request.Id);

            if (book == null)
            {
                return Fail<UpdateBookResponse>(ErrorCode.BookNotFound, "No Book was found with the provided Id");
            }

            book.Title = request.Title;
            book.Description = request.Description;
            book.Rating = request.Rating;
            book.PublishYear = request.PublishYear;

            var authors = new List<AuthorBooks>();

            foreach (var id in request.AuthorIds)
            {
                var author = await _authorRepository.GetById(id);
                if (author == null)
                {
                    return Fail<UpdateBookResponse>(ErrorCode.AuthorNotFound, "No Author was found with the provided Id");
                }
                authors.Add(new AuthorBooks() { AuthorId = id, BookId = request.Id });
            }

            var previousAuthors = await _bookRepository.GetBookAuthors(request.Id);
            await _authorBookRepository.RemoveRange(previousAuthors);
            await _authorBookRepository.AddRange(authors);

            if(request.Image != null)
            {
                var path = Path.Combine(_configuration["ImagesPath"], request.Image.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                    stream.Close();
                }

                book.ImagePath = request.Image.FileName;
            }

            await _bookRepository.Update(book);
            return Ok<UpdateBookResponse>();
        }

        public async Task<Response<DeleteBookResponse>> DeleteBook(string id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
            {
                return Fail<DeleteBookResponse>(ErrorCode.BookNotFound, "No Book was found with the provided Id");
            }

            await _bookRepository.Delete(book);
            return Ok<DeleteBookResponse>();
        }

        public async Task<Response<IEnumerable<BookModel>>> GetAllBooks()
        {
            var books = await _bookRepository.GetBooksWithAuthors();
            var result = books.Adapt<IEnumerable<BookModel>>();
            return Ok(result);
        }

        public async Task<Response<BookModel>> GetBookById(string id)
        {
            var book = await _bookRepository.GetBookByIdWithAuthors(id);
            if (book == null)
            {
                return Fail<BookModel>(ErrorCode.BookNotFound, "No Book was found with the provided Id");
            }

            var result = book.Adapt<BookModel>();
            return Ok(result);
        }

        public async Task<Response<ChangeBookStatusResponse>> ChangeBookStatus(string id)
        {
            var book = await _bookRepository.GetById(id);
            if(book == null)
            {
                return Fail<ChangeBookStatusResponse>(ErrorCode.BookNotFound, "No Book was found with the provided Id");
            }

            if (book.IsTaken == false)
                book.IsTaken = true;
            else
                book.IsTaken = false;
            await _bookRepository.Update(book);
            return Ok<ChangeBookStatusResponse>();
        }
    }
}
