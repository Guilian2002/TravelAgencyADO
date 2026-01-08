using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyADO.BLL.Services.Interfaces;
using TravelAgencyADO.Domain.Entities;
using TravelAgencyADO.Domain.Repositories;

namespace TravelAgencyADO.BLL.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;

        public BookingService(IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public ICollection<Booking> GetAll()
            => _bookingRepo.GetAll();

        public Booking? GetById(Guid bookingId)
            => _bookingRepo.GetById(bookingId);

        public Booking? Create(DateTime? bookingDate, string clientName, ICollection<Guid> activityIds)
        {
            if (bookingDate is null)
                throw new ArgumentException("BookingDate is required.", nameof(bookingDate));

            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentException("ClientName is required.", nameof(clientName));

            if (activityIds.IsNullOrEmpty())
                throw new ArgumentException("ActivityIds is required.", nameof(activityIds));

            var booking = new Booking(
                id: Guid.NewGuid(),
                bookingDate: bookingDate,
                clientName: clientName.Trim(),
                activityIds: activityIds
            );

            _bookingRepo.Insert(booking);

            return booking;
        }

        public bool Update(Guid bookingId, DateTime? bookingDate, string clientName)
        {
            if (bookingId == Guid.Empty)
                throw new ArgumentException("BookingId is required.", nameof(bookingId));

            if (bookingDate is null)
                throw new ArgumentException("BookingDate is required.", nameof(bookingDate));

            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentException("ClientName is required.", nameof(clientName));

            var booking = new Booking(
                id: Guid.NewGuid(),
                bookingDate: bookingDate,
                clientName: clientName.Trim()
            );

            return _bookingRepo.Update(booking);
        }

        public bool Delete(Guid bookingId)
            => _bookingRepo.Delete(bookingId);
    }
}

