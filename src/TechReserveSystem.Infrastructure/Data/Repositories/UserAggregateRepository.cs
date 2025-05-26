using Microsoft.EntityFrameworkCore;
using TechReserveSystem.Domain.Aggregates;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.ValueObjects;
using TechReserveSystem.Infrastructure.Data.Context;

namespace TechReserveSystem.Infrastructure.Data.Repositories
{
    public class UserAggregateRepository : IUserAggregateRepository
    {
        private readonly AppDbContext _dbContext;
        public UserAggregateRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserAggregate?> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                return null;

            var userReservations = await _dbContext.UserReservations
            .Where(ur => ur.UserId == id)
            .ToListAsync();

            var reservationAggregates = userReservations
        .Select(r => ReservationAggregate.FromDatabase(
            r.ReservationId,
            r.UserId,
            r.EquipmentId,
            r.Quantity,
            new(r.StartDate, r.ExpectedReturnDate),
           ReservationStatus.Approved,
            r.CreatedAt))
        .ToList();
            var userAgragate = new UserAggregate(user.Id, user.Name, user.Email, user.IsActive, user.CreatedAt, user.Role, reservationAggregates);

            return userAgragate;
        }
        public async Task<IReadOnlyCollection<ReservationAggregate>> GetUserReservationsAsync(Guid userId)
        {
            var userReservations = await _dbContext.UserReservations
           .Where(ur => ur.UserId == userId)
           .ToListAsync();

            var userReservationAggregates = userReservations
          .Select(r => ReservationAggregate.FromDatabase(
              r.ReservationId,
              r.UserId,
              r.EquipmentId,
              r.Quantity,
              new(r.StartDate, r.ExpectedReturnDate),
             ReservationStatus.Approved,
              r.CreatedAt))
          .ToList();

            return userReservationAggregates;
        }
    }
}