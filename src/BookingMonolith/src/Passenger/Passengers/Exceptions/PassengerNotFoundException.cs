using System.Net;
using Griffin.Core.Exception;

namespace BookingMonolith.Passenger.Passengers.Exceptions;

public class PassengerNotFoundException : AppException
{
    public PassengerNotFoundException() : base("Passenger not found!", HttpStatusCode.NotFound)
    {
    }
}