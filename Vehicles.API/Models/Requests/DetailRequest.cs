namespace Vehicles.API.Models.Requests
{
    public class DetailRequest
    {
        public int Id { get; set; }

        public int HistoryId { get; set; }

        public int ProcedureId { get; set; }

        public decimal LaborPrice { get; set; }

        public decimal SparePartsPrice { get; set; }

        public string Remarks { get; set; }
    }
}
