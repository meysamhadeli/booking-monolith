using System.Net;
using Griffin.Core.Exception;

namespace BookingMonolith.Flight.Flights.Exceptions;

public class FlightNotFountException : AppException
{
    public FlightNotFountException() : base("Flight not found!", HttpStatusCode.NotFound)
    {
    }
}