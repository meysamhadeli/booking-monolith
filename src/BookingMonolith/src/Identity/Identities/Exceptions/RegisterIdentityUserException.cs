using Griffin.Core.Exception;

namespace BookingMonolith.Identity.Identities.Exceptions;

public class RegisterIdentityUserException : AppException
{
    public RegisterIdentityUserException(string message) : base(message)
    {
    }
}