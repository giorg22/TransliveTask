using Application.Authors.Models;
using Application.Authors.Requests;
using Application.Authors.Responses;
using Application.Books.Model;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Shared.ReponseExtensions;

namespace Application.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<Response<string>> CreateAuthor(AddAuthorRequest request)
        {
            var author = request.Adapt<Author>();
            var result = await _authorRepository.Add(author);
            return Ok(result);
        }

        public async Task<Response<DeleteAuthorResponse>> DeleteAuthor(string id)
        {
            var author = await _authorRepository.GetById(id);
            if(author == null)
            {
                return Fail<DeleteAuthorResponse>(ErrorCode.NotFound, "No Author was found with the provided Id");
            }

            await _authorRepository.Delete(author);
            return Ok<DeleteAuthorResponse>();
        }

        public async Task<Response<IEnumerable<GetAuthorModel>>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAuthorsWithBooks();
            var result = authors.Adapt<IEnumerable<GetAuthorModel>>();
            return Ok(result);
        }

        public async Task<Response<Author>> GetAuthorById(string id)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return Fail<Author>(ErrorCode.NotFound, "No Author was found with the provided Id");
            }
            
            return Ok(author);
        }
    }
}