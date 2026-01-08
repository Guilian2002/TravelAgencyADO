using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyADO.Domain.Entities;

namespace TravelAgencyADO.BLL.Services.Interfaces
{
    public interface IBookingService
    {
        ICollection<Booking> GetAll();
        Booking? GetById(Guid bookingId);
        Booking? Create(DateTime? bookingDate, string clientName, ICollection<Guid> activityIds);
        bool Update(Guid bookingId, DateTime? bookingDate, string clientName);
        bool Delete(Guid bookingId);
    }
}
