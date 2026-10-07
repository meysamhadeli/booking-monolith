using BookingMonolith.Passenger.Passengers.Models;
using Griffin.Mongo;
using Humanizer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookingMonolith.Passenger.Data;

public class PassengerReadDbContext : MongoDbContext
{
    public PassengerReadDbContext(IOptions<MongoOptions> options) : base(options)
    {
        Passenger = GetCollection<PassengerReadModel>(nameof(Passenger).Underscore());
    }

    public IMongoCollection<PassengerReadModel> Passenger { get; }
}