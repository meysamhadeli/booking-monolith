using Griffin.Core.Model;
using Microsoft.AspNetCore.Identity;

namespace BookingMonolith.Identity.Identities.Models;

public class UserToken : IdentityUserToken<Guid>, IVersion
{
    public long Version { get; set; }
}