﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Data.Entities
{
    public class VehiclePhoto
    {
        public int Id { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:44389/images/noimage.png"
            : $"https://vehiclesprep.blob.core.windows.net/vehicles/{ImageId}";
    }
}