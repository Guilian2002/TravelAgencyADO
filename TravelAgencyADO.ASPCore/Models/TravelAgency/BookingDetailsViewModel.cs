namespace TravelAgencyADO.ASPCore.Models.TravelAgency
{
    public class BookingDetailsViewModel
    {
        public BookingViewModel Booking { get; set; }
        public DestinationViewModel Destination { get; set; }
        public List<ActivityViewModel> Activities { get; set; }
    }
}
