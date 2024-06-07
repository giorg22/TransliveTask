using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationDBContext _context;
        public AuthorRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthorsWithBooks()
        {
            return await _context.Authors.Include(x => x.AuthorBooks).ThenInclude(x => x.Book).ToListAsync();
        }
    }
}
