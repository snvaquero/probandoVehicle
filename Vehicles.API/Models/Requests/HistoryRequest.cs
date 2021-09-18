namespace Vehicles.API.Models.Requests
{
    public class HistoryRequest
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public int Mileage { get; set; }

        public string Remarks { get; set; }

        public string UserId { get; set; }
    }
}
