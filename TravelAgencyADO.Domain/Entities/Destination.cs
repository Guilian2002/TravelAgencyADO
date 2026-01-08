using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyADO.Domain.Entities
{
    public class Destination
    {
        public Guid Id { get; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Description { get; private set; }

        public ICollection<Activity> Activities { get; private set; } = new List<Activity>();

        public Destination(Guid id, string country, string city, string description)
        {
            Id = id;
            Country = country;
            City = city;
            Description = description;
        }

        public Destination(string country, string city, string description)
        {
            Id = Guid.NewGuid();
            Country = country;
            City = city;
            Description = description;
        }
    }
}
