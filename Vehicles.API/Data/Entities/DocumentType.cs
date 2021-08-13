using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Data.Entities
{
    public class DocumentType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
