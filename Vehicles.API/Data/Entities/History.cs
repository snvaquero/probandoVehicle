using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Data.Entities
{
    public class History
    {
        public int Id { get; set; }

        [Display(Name = "Vehículo")]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }

        [Display(Name = "Kilometraje")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Mileage { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public ICollection<Detail> Details { get; set; }

        [Display(Name = "# Detalles")]
        public int DetailsCount => Details == null ? 0 : Details.Count;
    }
}
