using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryApi.Domain;
using LibraryApi.Filters;
using LibraryApi.Models.Reservations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class ReservationsController : ControllerBase
    {

        private readonly LibraryDataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;

        public ReservationsController(LibraryDataContext context, IMapper mapper, MapperConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        // POST /reservations
        [HttpPost("/reservations")]
        [ResponseCache(Location =ResponseCacheLocation.Client, Duration = 5)]
        [ValidateModel]
        public async Task<ActionResult> CreateReservation([FromBody] PostReservationRequest request)
        {
            // Update the domain (POST is unsafe - it does work. What work will we do?)
            // -- Create and Process a new Reservation (in our synch model)
            // -- Save it to the database.
          
            var reservation = _mapper.Map<Reservation>(request);
            // Fake Processing the Order
            //  - Each book takes 1 second.
            var numberOfBooks = reservation.Books.Split(',').Length;
            for(var t = 0; t<numberOfBooks; t++)
            {
                await Task.Delay(1000);
            }
            reservation.Status = ReservationStatus.Accepted;
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
    
            var response = _mapper.Map<GetReservationDetailsResponse>(reservation);
            
            return CreatedAtRoute("reservations#getareservation", new { id = response.Id }, response);
        }

        // GET /reservations/{id}
        [HttpGet("/reservations/{id:int}", Name ="reservations#getareservation")]
        public async Task<ActionResult<GetReservationDetailsResponse>> GetAReservation(int id)
        {
            var response = await _context.Reservations
                .ProjectTo<GetReservationDetailsResponse>(_config)
                .SingleOrDefaultAsync(r => r.Id == id);
          
            return this.Maybe(response); // I committed a sin here. See if you can find it!
        }
        // Async
    }
}
