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
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepo _activityRepo;

        public ActivityService(IActivityRepo activityRepo)
        {
            _activityRepo = activityRepo;
        }

        public ICollection<Activity> GetAll(Guid destinationId)
            => _activityRepo.GetAll(destinationId);

        public Activity? GetById(Guid activityId)
            => _activityRepo.GetById(activityId);

        public Activity? Create(string title, string description, decimal price, Guid destinationId)
        {

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            if (price == 0)
                throw new ArgumentException("Price is required.", nameof(price));

            if (destinationId == Guid.Empty)
                throw new ArgumentException("DestinationId is required.", nameof(destinationId));

            var activity = new Activity(
                id: Guid.NewGuid(),
                title: title.Trim(),
                description: description.Trim(),
                price: price,
                destinationId: destinationId
            );

            _activityRepo.Insert(activity);

            return activity;
        }

        public bool Update(Guid activityId, string title, string description, decimal price)
        {
            if (activityId == Guid.Empty)
                throw new ArgumentException("ActivityId is required.", nameof(activityId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            if (price == 0)
                throw new ArgumentException("Price is required.", nameof(price));

            var activity = new Activity(
                id: activityId,
                title: title.Trim(),
                description: description.Trim(),
                price: price
            );

            return _activityRepo.Update(activity);
        }

        public bool Delete(Guid activityId)
            => _activityRepo.Delete(activityId);
    }
}
