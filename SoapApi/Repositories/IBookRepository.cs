using System.Collections.Generic;
using SoapApi.Models;

namespace SoapApi.Repositories
{
    public interface IBookRepository
    {
        IList<BookModel> GetBooksByName(string name);
    }
}
