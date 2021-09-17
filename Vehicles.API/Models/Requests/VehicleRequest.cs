namespace Vehicles.API.Models.Requests
{
    public class VehicleRequest
    {
        public int Id { get; set; }

        public int VehicleTypeId { get; set; }

        public int BrandId { get; set; }

        public int Model { get; set; }

        public string Plaque { get; set; }

        public string Line { get; set; }

        public string Color { get; set; }

        public string UserId { get; set; }

        public string Remarks { get; set; }

        public byte[] Image { get; set; }
    }
}
