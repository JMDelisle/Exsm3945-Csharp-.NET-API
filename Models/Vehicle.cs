namespace API_Assignment.Models
{
    public class Vehicle
    {
        public string VIN { get; set; }
        public int ModelID { get; set; }
        public int DealershipID { get; set; }
        public string TrimLevel { get; set; }

        public virtual VehicleModel VehicleModel { get; set; } = null!;
        public virtual Dealership Dealership { get; set; } = null!;
    }
}
