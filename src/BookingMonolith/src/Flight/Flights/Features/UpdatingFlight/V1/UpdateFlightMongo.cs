using Ardalis.GuardClauses;
using BookingMonolith.Flight.Data;
using BookingMonolith.Flight.Flights.Exceptions;
using BookingMonolith.Flight.Flights.Models;
using BuildingBlocks.Core.CQRS;
using BuildingBlocks.Core.Event;
using MapsterMapper;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BookingMonolith.Flight.Flights.Features.UpdatingFlight.V1;

public record UpdateFlightMongo(Guid Id, string FlightNumber, Guid AircraftId, DateTime DepartureDate,
                                Guid DepartureAirportId, DateTime ArriveDate, Guid ArriveAirportId, decimal DurationMinutes, DateTime FlightDate,
                                Enums.FlightStatus Status, decimal Price, bool IsDeleted = false) : InternalCommand;


internal class UpdateFlightMongoCommandHandler : ICommandHandler<UpdateFlightMongo>
{
    private readonly FlightReadDbContext _flightReadDbContext;
    private readonly IMapper _mapper;

    public UpdateFlightMongoCommandHandler(
        FlightReadDbContext flightReadDbContext,
        IMapper mapper)
    {
        _flightReadDbContext = flightReadDbContext;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateFlightMongo request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var flightReadModel = _mapper.Map<FlightReadModel>(request);

        var flight = await _flightReadDbContext.Flight.AsQueryable()
            .FirstOrDefaultAsync(x => x.FlightId == flightReadModel.FlightId && !x.IsDeleted, cancellationToken);

        if (flight is null)
        {
            throw new FlightNotFountException();
        }

        await _flightReadDbContext.Flight.UpdateOneAsync(
            x => x.FlightId == flightReadModel.FlightId,
            Builders<FlightReadModel>.Update
                .Set(x => x.Price, flightReadModel.Price)
                .Set(x => x.ArriveDate, flightReadModel.ArriveDate)
                .Set(x => x.AircraftId, flightReadModel.AircraftId)
                .Set(x => x.DurationMinutes, flightReadModel.DurationMinutes)
                .Set(x => x.DepartureDate, flightReadModel.DepartureDate)
                .Set(x => x.FlightDate, flightReadModel.FlightDate)
                .Set(x => x.FlightNumber, flightReadModel.FlightNumber)
                .Set(x => x.IsDeleted, flightReadModel.IsDeleted)
                .Set(x => x.Status, flightReadModel.Status)
                .Set(x => x.ArriveAirportId, flightReadModel.ArriveAirportId)
                .Set(x => x.DepartureAirportId, flightReadModel.DepartureAirportId),
            cancellationToken: cancellationToken);

        return Unit.Value;
    }
}
