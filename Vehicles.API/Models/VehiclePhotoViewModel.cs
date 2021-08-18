using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Models
{
    public class VehiclePhotoViewModel : VehiclePhoto
    {
        public int VehicleId { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public IFormFile ImageFile { get; set; }
    }
}
