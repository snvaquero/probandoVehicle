using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Models
{
    public class DetailViewModel : Detail
    {
        public int HistoryId { get; set; }

        [Display(Name = "Procedimiento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un procedimiento.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ProcedureId { get; set; }

        public IEnumerable<SelectListItem> Procedures { get; set; }
    }
}
