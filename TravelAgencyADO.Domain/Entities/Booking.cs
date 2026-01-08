using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyADO.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; }
        public DateTime? BookingDate { get; private set; }
        public string ClientName { get; private set; }
        public ICollection<Guid> ActivityIds { get; private set; } = new List<Guid>();

        public Booking(Guid id, DateTime? bookingDate, string clientName, ICollection<Guid> activityIds)
        {
            Id = id;
            BookingDate = bookingDate;
            ClientName = clientName;
            ActivityIds = activityIds.ToList() ?? new List<Guid>();
        }

        public Booking(DateTime? bookingDate, string clientName, ICollection<Guid> activityIds)
        {
            Id = Guid.NewGuid();
            BookingDate = bookingDate;
            ClientName = clientName;
            ActivityIds = activityIds.ToList() ?? new List<Guid>();
        }

        public Booking(Guid id, DateTime? bookingDate, string clientName)
        {
            Id = id;
            BookingDate = bookingDate;
            ClientName = clientName;
        }
    }
}
