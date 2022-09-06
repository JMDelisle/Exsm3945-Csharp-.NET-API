using API_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Assignment.Data
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<Dealership> Dealerships { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleModel> Models { get; set; }
        public virtual DbSet<VehicleManufacturer> Manufacturers{ get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseMySql("server=localhost;user=root;database=api_assignment", ServerVersion.Parse("10.4.24-mariadb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseCollation("utf8mb4_general_ci").HasCharSet("utf8mb4");

            modelBuilder.Entity<VehicleManufacturer>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable("manufacturer");
                entity.Property(e => e.ID).HasColumnType("int(11)").HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
                entity.HasData(new VehicleManufacturer[] {
                    new VehicleManufacturer() { ID = 1, Name = "Ford" },
                    new VehicleManufacturer() { ID = 2, Name = "Chevrolet" },
                    new VehicleManufacturer() { ID = 3, Name = "Dodge" },
                    new VehicleManufacturer() { ID = 4, Name = "Honda" },
                    new VehicleManufacturer() { ID = 5, Name = "Toyota" }
                });
            });
            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable("model");
                entity.HasIndex(e => e.ManufacturerID, "FK_Model_Manufacturer");
                entity.Property(e => e.ID).HasColumnType("int(11)").HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.ManufacturerID).HasColumnType("int(11)").HasColumnName("manufacturerid");
                entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
                entity.HasOne(model => model.VehicleManufacturer).WithMany(manufacturer => manufacturer.VehicleModels).HasForeignKey(model => model.ManufacturerID)
                    .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Model_Manufacturer");
                entity.HasData(new VehicleModel[] {
                    new VehicleModel() { ID = 1, ManufacturerID = 1, Name = "Mustang" },
                    new VehicleModel() { ID = 2, ManufacturerID = 1, Name = "F-150" },
                    new VehicleModel() { ID = 3, ManufacturerID = 2, Name = "Corvette" },
                    new VehicleModel() { ID = 4, ManufacturerID = 2, Name = "Camaro" },
                    new VehicleModel() { ID = 5, ManufacturerID = 3, Name = "Challenger" },
                    new VehicleModel() { ID = 6, ManufacturerID = 3, Name = "Charger" },
                    new VehicleModel() { ID = 7, ManufacturerID = 4, Name = "Civic" },
                    new VehicleModel() { ID = 8, ManufacturerID = 4, Name = "Accord" },
                    new VehicleModel() { ID = 9, ManufacturerID = 5, Name = "Corolla" },
                    new VehicleModel() { ID = 10, ManufacturerID = 5, Name = "Camry" } 
                });
            });
            modelBuilder.Entity<Dealership>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable("dealership");
                entity.HasIndex(e => e.ManufacturerID, "FK_Dealership_Manufacturer");
                entity.Property(e => e.ID).HasColumnType("int(11)").HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
                entity.Property(e => e.ManufacturerID).HasColumnType("int(11)").HasColumnName("manufacturerid");
                entity.Property(e => e.Address).HasMaxLength(50).HasColumnName("address");
                entity.Property(e => e.PhoneNumber).HasColumnType("char(10)").HasColumnName("phonenumber");
                entity.HasOne(dealership => dealership.VehicleManufacturer).WithMany(manufacturer => manufacturer.Dealerships).HasForeignKey(dealership => dealership.ManufacturerID)
                    .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Dealership_Manufacturer");
                entity.HasData(new Dealership[] {
                    new Dealership() {ID=1, Name = "Joe's Discount Fords", ManufacturerID = 1, Address = "123 Cool St", PhoneNumber = "8005551234"}
                });
            });
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.VIN);
                entity.ToTable("vehicle");
                entity.HasIndex(e => e.ModelID, "FK_Vehicle_Model");
                entity.HasIndex(e => e.DealershipID, "FK_Vehicle_Dealership");
                entity.Property(e => e.VIN).HasColumnType("char(17)").HasColumnName("vin");
                entity.Property(e => e.ModelID).HasColumnType("int(11)").HasColumnName("modelid");
                entity.Property(e => e.TrimLevel).HasMaxLength(50).HasColumnName("trimlevel");
                entity.HasOne(vehicle => vehicle.VehicleModel).WithMany(model => model.Vehicles).HasForeignKey(vehicle => vehicle.ModelID)
                    .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Vehicle_Model");
                entity.HasOne(vehicle => vehicle.Dealership).WithMany(dealership => dealership.Vehicles).HasForeignKey(vehicle => vehicle.DealershipID)
                    .OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_Vehicle_Dealership");

                entity.HasData(new Vehicle[] {
                    new Vehicle() { VIN="1FA6P8TH4J5102322", TrimLevel="Ecoboost", ModelID = 1, DealershipID = 1 },
                    new Vehicle() { VIN="2C3CDXCT7JH260378", TrimLevel="R/T", ModelID = 6 , DealershipID = 1},
                    new Vehicle() { VIN="2HGFC3A51LH220441", TrimLevel="SI", ModelID = 7 , DealershipID = 1}
                });
            });


            

            

            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}