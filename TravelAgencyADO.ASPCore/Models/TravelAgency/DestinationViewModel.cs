using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelAgencyADO.ASPCore.Models.TravelAgency
{
    public class DestinationViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Pays : ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Le pays est obligatoire!")]
        [StringLength(150, ErrorMessage = "Le pays doit contenir moins de 150 caractères.")]
        public string Country { get; set; } = string.Empty;

        [DisplayName("Ville : ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "La ville est obligatoire!")]
        [StringLength(100, ErrorMessage = "Le ville doit contenir moins de 100 caractères.")]
        public string City { get; set; } = string.Empty;

        [DisplayName("Description : ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "La description est obligatoire!")]
        [StringLength(255, ErrorMessage = "La description doit contenir moins de 255 caractères.")]
        public string Description { get; set; } = string.Empty;
    }
}
