using Domain.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorsWithBooks();
        Task<Author> GetAuthorByIdWithBooks(string id);
    }
}
