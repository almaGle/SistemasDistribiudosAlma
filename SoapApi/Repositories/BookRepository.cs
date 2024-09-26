using SoapApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoapApi.Infrastructure;


namespace SoapApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RelationalDbContext _dbContext;

        public BookRepository(RelationalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<BookModel> GetBooksByName(string name)
        {
            var bookEntities = _dbContext.Books
                .Where(b => EF.Functions.Like(b.Title, $"%{name}%"))
                .AsNoTracking()
                .ToList();

            // Mapear BookEntity a BookModel
            var bookModels = bookEntities.Select(bookEntity => new BookModel
            {
                Id = bookEntity.Id,
                Title = bookEntity.Title,
                Author = bookEntity.Author,
                PublishedDate = bookEntity.PublishedDate
            }).ToList();

            return bookModels;
        }
    }
}
