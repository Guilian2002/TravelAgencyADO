using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyADO.Domain.Entities;

namespace TravelAgencyADO.Domain.Repositories
{
    public interface IDestinationRepo
    {
        ICollection<Destination> GetAll();
        Destination? GetById(Guid destinationId);
        void Insert(Destination destination);
        bool Update(Destination destination);
        bool Delete(Guid destinationId);
    }
}
