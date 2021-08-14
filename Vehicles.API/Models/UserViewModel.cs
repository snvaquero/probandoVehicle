using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Models
{
    public class UserViewModel : User
    {
        [Display(Name = "Foto")]
        public IFormFile ImageFile { get; set; }
    }
}
