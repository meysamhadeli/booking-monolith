using Ardalis.GuardClauses;
using BookingMonolith.Flight.Airports.Exceptions;
using BookingMonolith.Flight.Airports.Models;
using BookingMonolith.Flight.Data;
using BuildingBlocks.Core.CQRS;
using BuildingBlocks.Core.Event;
using MapsterMapper;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BookingMonolith.Flight.Airports.Features.CreatingAirport.V1;

public record CreateAirportMongo(Guid Id, string Name, string Address, string Code, bool IsDeleted = false) : InternalCommand;

internal class CreateAirportMongoHandler : ICommandHandler<CreateAirportMongo>
{
    private readonly FlightReadDbContext _flightReadDbContext;
    private readonly IMapper _mapper;

    public CreateAirportMongoHandler(
        FlightReadDbContext flightReadDbContext,
        IMapper mapper)
    {
        _flightReadDbContext = flightReadDbContext;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateAirportMongo request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var airportReadModel = _mapper.Map<AirportReadModel>(request);

        var aircraft = await _flightReadDbContext.Airport.AsQueryable()
            .FirstOrDefaultAsync(x => x.AirportId == airportReadModel.AirportId &&
                                      !x.IsDeleted, cancellationToken);

        if (aircraft is not null)
        {
            throw new AirportAlreadyExistException();
        }

        await _flightReadDbContext.Airport.InsertOneAsync(airportReadModel, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}
