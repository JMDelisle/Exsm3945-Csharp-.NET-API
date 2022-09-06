namespace API_Assignment.Models
{
    public class VehicleModel
    {
        public VehicleModel()
        {
            Vehicles = new HashSet<Vehicle>();
        }
        public int ID { get; set; }
        public int ManufacturerID { get; set; }
        public string Name { get; set;  }

        public virtual VehicleManufacturer VehicleManufacturer { get; set; } = null!;
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
