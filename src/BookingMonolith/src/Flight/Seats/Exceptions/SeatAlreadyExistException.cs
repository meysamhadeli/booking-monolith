using System.Net;
using Griffin.Core.Exception;

namespace BookingMonolith.Flight.Seats.Exceptions;

public class SeatAlreadyExistException : AppException
{
    public SeatAlreadyExistException(int? code = default) : base("Seat already exist!", HttpStatusCode.Conflict, code)
    {
    }
}