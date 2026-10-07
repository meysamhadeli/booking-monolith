using System.Net;
using Griffin.Core.Exception;

namespace BookingMonolith.Flight.Flights.Exceptions;

public class FlightAlreadyExistException : AppException
{
    public FlightAlreadyExistException(int? code = default) : base("Flight already exist!", HttpStatusCode.Conflict, code)
    {
    }
}