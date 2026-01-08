using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyADO.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

        public Guid DestinationId { get; }
        public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

        public Activity(Guid id, string title, string description, decimal price, Guid destinationId)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            DestinationId = destinationId;
        }

        public Activity(string title, string description, decimal price, Guid destinationId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Price = price;
            DestinationId = destinationId;
        }
        public Activity(Guid id, string title, string description, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }
    }
}
