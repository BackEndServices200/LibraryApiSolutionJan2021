using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryApi.Domain;
using LibraryApi.Models.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class EfSqlBooks : ILookupBooks
    {
        private LibraryDataContext _context;
        private IMapper _mapper;
        private MapperConfiguration _config;

        public EfSqlBooks(LibraryDataContext context, IMapper mapper, MapperConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        public async Task<GetBookDetailsResponse> GetBookById(int id)
        {
            var response = await _context.GetBooksInInventory()
                .ProjectTo<GetBookDetailsResponse>(_config)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();
            return response;
        }
    }
}
