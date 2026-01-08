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
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepo _destinationRepo;

        public DestinationService(IDestinationRepo destinationRepo)
        {
            _destinationRepo = destinationRepo;
        }

        public ICollection<Destination> GetAll()
            => _destinationRepo.GetAll();

        public Destination? GetById(Guid destinationId)
            => _destinationRepo.GetById(destinationId);

        public Destination? Create(string country, string city, string description)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required.", nameof(country));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required.", nameof(city));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            var destination = new Destination(
                id: Guid.NewGuid(),
                country: country.Trim(),
                city: city.Trim(),
                description: description.Trim()
            );

            _destinationRepo.Insert(destination);

            return destination;
        }

        public bool Update(Guid destinationId, string country, string city, string description)
        {
            if (destinationId == Guid.Empty)
                throw new ArgumentException("DestinationId is required.", nameof(destinationId));

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required.", nameof(country));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required.", nameof(city));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            var destination = new Destination(
                id: destinationId,
                country: country.Trim(),
                city: city.Trim(),
                description: description.Trim()
            );

            return _destinationRepo.Update(destination);
        }

        public bool Delete(Guid destinationId)
            => _destinationRepo.Delete(destinationId);
    }
}