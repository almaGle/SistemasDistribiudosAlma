using SoapApi.Contracts;
using SoapApi.Repositories;
using SoapApi.Dtos;

namespace SoapApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IList<BookResponseDto> GetBooksByName(string name)
        {
            var books = _bookRepository.GetBooksByName(name);
            return books.Select(book => new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate
            }).ToList();
        }
    }
}
