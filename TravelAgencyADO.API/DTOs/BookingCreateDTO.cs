namespace TravelAgencyADO.API.DTOs
{
    public record BookingCreateDTO(DateTime? bookingDate, string clientName, ICollection<Guid> activityIds);
}
