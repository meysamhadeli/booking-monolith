using Griffin.Core.Exception;

namespace BookingMonolith.Flight.Seats.Exceptions;

public class SeatNumberIncorrectException : AppException
{
    public SeatNumberIncorrectException() : base("Seat number is incorrect!")
    {
    }
}