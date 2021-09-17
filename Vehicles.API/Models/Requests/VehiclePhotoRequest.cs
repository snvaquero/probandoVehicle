namespace Vehicles.API.Models.Requests
{
    public class VehiclePhotoRequest
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public byte[] Image { get; set; }
    }
}
