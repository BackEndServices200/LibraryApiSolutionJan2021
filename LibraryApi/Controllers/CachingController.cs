using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class CachingController : ControllerBase
    {
        private readonly ICatalog _catalog;

        public CachingController(ICatalog catalog)
        {
            _catalog = catalog;
        }

        [HttpGet("caching/catalog")]
        public async Task<ActionResult> GetTheCatalog()
        {
            CatalogModel catalog = await _catalog.GetTheCatalog();
            return Ok(catalog);
        }

        [HttpGet("caching/info")]
        [ResponseCache(Duration = 15, Location = ResponseCacheLocation.Any)]
        public ActionResult GetSomeInfo()
        {
            return Ok(new
            {
                Message = "Hello from the server",
                CreatedAt = DateTime.Now
            });
        }
    }
}
