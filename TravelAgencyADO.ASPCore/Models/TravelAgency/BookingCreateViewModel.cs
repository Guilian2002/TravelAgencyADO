using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyADO.ASPCore.Models.TravelAgency
{
    public class BookingCreateViewModel
    {
        [DisplayName("Nom du client : ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Le nom du client est obligatoire!")]
        [StringLength(70, ErrorMessage = "Le nom du client doit contenir moins de 70 caractères.")]
        public string ClientName { get; set; }

        [DisplayName("Date de réservation : ")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La date de réservation est obligatoire!")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public List<Guid> SelectedActivityIds { get; set; } = new List<Guid>();
        public List<ActivityRowViewModel> AvailableActivities { get; set; } = new();
        public string SelectedCountry { get; set; }
        public List<string> AvailableCountries { get; set; } = new();
    }

    public class ActivityRowViewModel
    {
        public Guid ActivityId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }

}
