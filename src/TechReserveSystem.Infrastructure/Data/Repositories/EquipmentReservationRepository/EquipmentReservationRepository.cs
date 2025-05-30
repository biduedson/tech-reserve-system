using Microsoft.EntityFrameworkCore;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Enuns;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository;
using TechReserveSystem.Infrastructure.Data.Context;

namespace TechReserveSystem.Infrastructure.Data.Repositories.EquipmentReservationRepository
{
    public class EquipmentReservationRepository : IEquipmentReservationRepository
    {
        private readonly AppDbContext _dbContext;
        public EquipmentReservationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UserExisting(Guid userId)
        {
            return await _dbContext.Users.FindAsync(userId) != null;
        }

        public async Task<bool> EquipmentExisting(Guid equipmentId)
        {
            return await _dbContext.Equipments.FindAsync(equipmentId) != null;
        }

        public async Task<int> GetUserOverdueReservationsCount(Guid userId)
        {
            return await _dbContext.EquipmentReservations
         .Where(r => r.UserId == userId
              && (r.Status == ReservationStatus.Approved.ToString()
              || r.Status == ReservationStatus.InProgress.ToString())
              && r.ExpectedReturnDate.Date < DateTime.Now.Date
              )
        .CountAsync();
        }
        public async Task<int> CountAvailableEquipmentOnDate(Guid equipmentId, DateTime reservationDate)
        {
            var stockQuantity = await _dbContext.Equipments
            .Where(e => e.Id == equipmentId)
            .Select(e => e.AvailableQuantity)
            .FirstOrDefaultAsync();

            var reservedCount = await _dbContext.EquipmentReservations
            .Where(reservation => reservation.EquipmentId == equipmentId
              && reservation.StartDate.Date == reservationDate.Date
              && reservation.Status == ReservationStatus.Approved.ToString())
            .CountAsync();
            return stockQuantity - reservedCount;
        }
        public async Task<EquipmentReservation?> GetById(Guid id) => await _dbContext.EquipmentReservations.FindAsync(id);

        public async Task<IEnumerable<EquipmentReservation?>> GetByUserId(Guid userId) => await _dbContext.EquipmentReservations
        .Where(reservation => reservation.UserId.Equals(userId))
        .ToListAsync();

        public async Task<IEnumerable<EquipmentReservation?>> GetByEquipmetId(Guid equipmentId) => await _dbContext.EquipmentReservations
        .Where(reservation => reservation.EquipmentId.Equals(equipmentId))
        .ToListAsync();

        public async Task<int> CountAvailableEquipmentOnDate(Equipment equipment, DateTime date)
        {
            var count = await _dbContext.EquipmentReservations
                .Where(reservation => reservation.StartDate.Date == date.Date
                    && reservation.EquipmentId == equipment.Id
                    && reservation.Status != ReservationStatus.Cancelled.ToString() // Exclui canceladas
                    && reservation.Status == ReservationStatus.Approved.ToString()) // Apenas reservas pendentes
                .CountAsync();

            return count;
        }

        public async Task<bool> HasRejectedReservationOnDate(Guid userId, Guid equipmentId, DateTime date)
        {
            return await _dbContext.EquipmentReservations
                .AnyAsync(reservation => reservation.UserId == userId
                     && reservation.EquipmentId == equipmentId
                     && reservation.StartDate.Date == date.Date
                     && reservation.Status == ReservationStatus.Rejected.ToString());
        }

        public async Task<IEnumerable<EquipmentReservation>> GetPendingReservationsByUser(Guid userId)
        {
            return await _dbContext.EquipmentReservations
         .Where(r => r.UserId == userId
              && (r.Status == ReservationStatus.Approved.ToString()
              || r.Status == ReservationStatus.InProgress.ToString())
              && r.ExpectedReturnDate.Date < DateTime.Now.Date
              )
        .ToListAsync();
        }

        public async Task<bool> HasUserAlreadyReservedEquipment(Guid userId, Guid equipmentId, DateTime reservationDate)
        {
            return await _dbContext.EquipmentReservations
            .AnyAsync(r => r.UserId == userId
                && r.EquipmentId == equipmentId
                && r.StartDate.Date == reservationDate.Date);
        }

        public async Task<IEnumerable<EquipmentReservation>> GetAll() => await _dbContext.EquipmentReservations.ToListAsync();

        public async Task<EquipmentReservation> Add(EquipmentReservation equipmentReservation)
        {
            await _dbContext.EquipmentReservations.AddAsync(equipmentReservation);
            return equipmentReservation;
        }

        public async Task<EquipmentReservation> Update(EquipmentReservation equipmentReservation)
        {
            _dbContext.EquipmentReservations.Update(equipmentReservation);
            await _dbContext.SaveChangesAsync();
            return equipmentReservation;
        }

        public void Delete(EquipmentReservation equipmentReservation) => _dbContext.EquipmentReservations.Remove(equipmentReservation);
    }
}