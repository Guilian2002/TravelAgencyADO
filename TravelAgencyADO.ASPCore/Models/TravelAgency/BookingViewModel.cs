using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelAgencyADO.ASPCore.Models.TravelAgency
{
    public class BookingViewModel
    {
        public Guid Id { get; set;  }

        [DisplayName("Date de réservation : ")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La date de réservation est obligatoire!")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BookingDate { get; set; }

        [DisplayName("Nom du client : ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Le nom du client est obligatoire!")]
        [StringLength(70, ErrorMessage = "Le nom du client doit contenir moins de 70 caractères.")]
        public string ClientName { get; set; } = string.Empty;
        public ICollection<Guid> ActivityIds { get; set; } = new List<Guid>();
    }
}
