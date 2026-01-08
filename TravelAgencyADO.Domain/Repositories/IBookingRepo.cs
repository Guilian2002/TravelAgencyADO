using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyADO.Domain.Entities;

namespace TravelAgencyADO.Domain.Repositories
{
    public interface IBookingRepo
    {
        ICollection<Booking> GetAll();
        Booking? GetById(Guid bookingId);
        void Insert(Booking booking);
        bool Update(Booking booking);
        bool Delete(Guid bookingId);
    }
}
