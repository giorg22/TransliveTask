using Application.Authors.Models;
using Application.Authors.Requests;
using Application.Authors.Responses;
using Application.Shared;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors
{
    public interface IAuthorService
    {
        Task<Response<string>> CreateAuthor(AddAuthorRequest request);
        Task<Response<UpdateAuthorResponse>> UpdateAuthor(UpdateAuthorRequest request);
        Task<Response<DeleteAuthorResponse>> DeleteAuthor(string id);
        Task<Response<IEnumerable<AuthorModel>>> GetAllAuthors();
        Task<Response<AuthorModel>> GetAuthorById(string id);
    }
}
