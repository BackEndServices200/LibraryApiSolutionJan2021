using LibraryApi.Filters;
using LibraryApi.Models.Reservations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class ReservationsController : ControllerBase
    {
        // POST /reservations
        [HttpPost("/reservations")]
        [ValidateModel]
        public ActionResult CreateReservation([FromBody] PostReservationRequest request)
        {
            // Update the domain (POST is unsafe - it does work. What work will we do?)
            // -- Create and Process a new Reservation (in our synch model)
            // -- Save it to the database.
            // PostReservationRequest -> Reservation -> GetReservationDetailsResponse
            // Return:
            //  - 201 Created
            //  - Location Header
            //  - A copy of what they'd get if they followed that header.
            //  - Bonus: A cache header!
            return Ok(request);
        }

        // GET /reservations/{id}

        // Async
    }
}
