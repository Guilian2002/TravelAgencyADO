using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyADO.Domain.Entities;

namespace TravelAgencyADO.BLL.Services.Interfaces
{
    public interface IActivityService
    {
        ICollection<Activity> GetAll(Guid destinationId);
        Activity? GetById(Guid activityId);
        Activity? Create(string title, string description, decimal price, Guid destinationId);
        bool Update(Guid activityId, string title, string description, decimal price);
        bool Delete(Guid activityId);
    }
}
