using Griffin.Core.Exception;

namespace BookingMonolith.Passenger.Exceptions;

public class InvalidPassengerIdException : DomainException
{
    public InvalidPassengerIdException(Guid passengerId)
        : base($"PassengerId: '{passengerId}' is invalid.")
    {
    }
}