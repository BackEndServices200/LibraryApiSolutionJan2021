using LibraryApi.Models.Books;
using System.Threading.Tasks;

namespace LibraryApi
{
    public interface ILookupBooks
    {
        Task<GetBookDetailsResponse> GetBookById(int id);
    }
}