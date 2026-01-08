namespace TravelAgencyADO.API.DTOs
{
    public record ActivityCreateDTO(string title, string description, decimal price, Guid destinationId);
}
