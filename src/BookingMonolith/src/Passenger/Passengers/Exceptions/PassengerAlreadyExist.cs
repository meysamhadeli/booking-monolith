using System.Net;
using Griffin.Core.Exception;

namespace BookingMonolith.Passenger.Passengers.Exceptions;

public class PassengerNotExist : AppException
{
    public PassengerNotExist() : base("Please register before!", HttpStatusCode.NotFound)
    {
    }
}