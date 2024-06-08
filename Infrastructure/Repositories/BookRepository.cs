using Domain.Base;
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
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly ApplicationDBContext _context;
        public BookRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> GetBooksWithAuthors()
        {
            return await _context.Books.Include(x => x.AuthorBooks).ThenInclude(x => x.Author).ToListAsync();
        }

        public async Task<Book> GetBookByIdWithAuthors(string id)
        {
            return await _context.Books.Include(x => x.AuthorBooks).ThenInclude(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AuthorBooks>> GetBookAuthors(string id)
        {
            return await _context.AuthorBooks.Where(x => x.BookId == id).ToListAsync();
        }
    }
}
