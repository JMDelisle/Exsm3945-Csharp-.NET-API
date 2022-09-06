namespace API_Assignment.Models
{
    public class Dealership
    {
        public Dealership()
        {
            Vehicles = new HashSet<Vehicle>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int ManufacturerID { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public virtual VehicleManufacturer VehicleManufacturer { get; set; } = null!;
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
