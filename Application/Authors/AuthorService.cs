using Application.Auth.Requests;
using Application.Authors.Models;
using Application.Authors.Requests;
using Application.Authors.Responses;
using Application.Books.Model;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
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
            AddAuthorRequestValidator validator = new AddAuthorRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Fail<string>(ErrorCode.ValidationFailed, validationResult);
            }

            var author = request.Adapt<Author>();
            var result = await _authorRepository.Add(author);
            return Ok(result);
        }

        public async Task<Response<UpdateAuthorResponse>> UpdateAuthor(UpdateAuthorRequest request)
        {
            UpdateAuthorRequestValidator validator = new UpdateAuthorRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Fail<UpdateAuthorResponse>(ErrorCode.ValidationFailed, validationResult);
            }

            var author = await _authorRepository.GetById(request.Id);

            if (author == null)
            {
                return Fail<UpdateAuthorResponse>(ErrorCode.AuthorNotFound, "No Author was found with the provided Id");
            }

            author.Firstname = request.Firstname;
            author.Lastname = request.Lastname;
            author.BirthYear = request.BirthYear;

            await _authorRepository.Update(author);
            return Ok<UpdateAuthorResponse>();
        }

        public async Task<Response<DeleteAuthorResponse>> DeleteAuthor(string id)
        {
            var author = await _authorRepository.GetById(id);
            if(author == null)
            {
                return Fail<DeleteAuthorResponse>(ErrorCode.AuthorNotFound, "No Author was found with the provided Id");
            }

            await _authorRepository.Delete(author);
            return Ok<DeleteAuthorResponse>();
        }

        public async Task<Response<IEnumerable<AuthorModel>>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAll();
            var result = authors.Adapt<IEnumerable<AuthorModel>>();
            return Ok(result);
        }

        public async Task<Response<AuthorModel>> GetAuthorById(string id)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return Fail<AuthorModel>(ErrorCode.AuthorNotFound, "No Author was found with the provided Id");
            }

            var result = author.Adapt<AuthorModel>();
            return Ok(result);
        }
    }
}