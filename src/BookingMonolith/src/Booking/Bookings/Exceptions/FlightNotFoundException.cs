using System.Net;
using Griffin.Core.Exception;

namespace BookingMonolith.Booking.Bookings.Exceptions;

public class FlightNotFoundException : AppException
{
    public FlightNotFoundException() : base("Flight doesn't exist!", HttpStatusCode.NotFound)
    {
    }
}