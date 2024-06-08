using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthorBookRepository : BaseRepository<AuthorBooks>, IAuthorBookRepository
    {
        public AuthorBookRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
