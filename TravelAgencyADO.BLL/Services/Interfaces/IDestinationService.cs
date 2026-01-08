using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyADO.Domain.Entities;

namespace TravelAgencyADO.BLL.Services.Interfaces
{
    public interface IDestinationService
    {
        ICollection<Destination> GetAll();
        Destination? GetById(Guid destinationId);
        Destination? Create(string country, string city, string description);
        bool Update(Guid destinationId, string country, string city, string description);
        bool Delete(Guid destinationId);
    }
}
