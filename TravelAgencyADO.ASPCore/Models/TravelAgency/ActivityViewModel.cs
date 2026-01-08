using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelAgencyADO.ASPCore.Models.TravelAgency
{
    public class ActivityViewModel
    {

        public Guid Id { get; set; }

        [DisplayName("Titre : ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Le titre est obligatoire!")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Le titre doit contenir entre 3 et 150 caractères.")]
        public string Title { get; set; } = string.Empty;

        [DisplayName("Prix : ")]
        [Required(ErrorMessage = "Le prix est obligatoire !")]
        [Range(0.01, 1000000, ErrorMessage = "Le prix doit être compris entre 0.01 et 1 000 000.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DisplayName("Description : ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "La description est obligatoire!")]
        [StringLength(200, ErrorMessage = "La description doit contenir moins de 200 caractères.")]
        public string Description { get; set; } = string.Empty;

        public Guid DestinationId { get; set; }
    }
}
