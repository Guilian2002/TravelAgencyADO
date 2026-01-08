using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyADO.Domain.Entities;

namespace TravelAgencyADO.Domain.Repositories
{
    public interface IActivityRepo
    {
        ICollection<Activity> GetAll(Guid destinationId);
        Activity? GetById(Guid activityId);
        void Insert(Activity activity);
        bool Update(Activity activity);
        bool Delete(Guid activityId);
    }
}
