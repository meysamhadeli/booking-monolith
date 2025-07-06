using Ardalis.GuardClauses;
using BookingMonolith.Flight.Data;
using BookingMonolith.Flight.Seats.Models;
using BuildingBlocks.Core.CQRS;
using BuildingBlocks.Core.Event;
using MapsterMapper;
using MediatR;
using MongoDB.Driver;

namespace BookingMonolith.Flight.Seats.Features.ReservingSeat.V1;

public record ReserveSeatMongo(Guid Id, string SeatNumber, Enums.SeatType Type,
                               Enums.SeatClass Class, Guid FlightId, bool IsDeleted = false) : InternalCommand;

internal class ReserveSeatMongoHandler : ICommandHandler<ReserveSeatMongo>
{
    private readonly FlightReadDbContext _flightReadDbContext;
    private readonly IMapper _mapper;

    public ReserveSeatMongoHandler(
        FlightReadDbContext flightReadDbContext,
        IMapper mapper)
    {
        _flightReadDbContext = flightReadDbContext;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(ReserveSeatMongo command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        var seatReadModel = _mapper.Map<SeatReadModel>(command);

        await _flightReadDbContext.Seat.UpdateOneAsync(
            x => x.SeatId == seatReadModel.SeatId,
            Builders<SeatReadModel>.Update
                .Set(x => x.IsDeleted, seatReadModel.IsDeleted),
            cancellationToken: cancellationToken);

        return Unit.Value;
    }
}
