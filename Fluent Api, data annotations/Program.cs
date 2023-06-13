using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Console.WriteLine("salam");

using (var db = new ProductContext())
{
    var country = new Country() { Name = "Azerbaycan" };
    var manufacturer = new Manufacturer() { Name = "Milla", country = country };
    var product = new Product() { Name = "ayran", Manufacturer = manufacturer, Price = 1 };
    db.products.Add(product);
    db.SaveChanges();
}

public class Product
{
    //[Key] <<< bu cur yazarak primary key ede bilirk
    public int ProductNumber { get; set; }
    [Required]

    //[Index( nameof("UName"), IsUnique = true)]

    public string? Name { get; set; }
    public float Price { get; set; }
    [NotMapped]
    public string Descripton { get; set; }
    public int ManufacturerId { get; set; }

    public Manufacturer? Manufacturer { get; set; }
}
//[NotMapped]
public class Country
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}
public class Manufacturer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int countryId { get; set; }
    public Country? country { get; set; }
}

public class ProductContext : DbContext
{
    public DbSet<Product> products { get; set; }
    public ProductContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FluentApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>().Property(m => m.Name).IsRequired();
       // bele etikde o classi DB yazmir 
        modelBuilder.Ignore<Country>();
        // bele etikde o classi DB yazir
        //modelBuilder.Entity<Product>();
        modelBuilder.Entity<Product>().HasKey(p => p.ProductNumber); //<< Bu curde primary key ede bilirik
        modelBuilder.Entity<Product>( 
            entity =>
            {
                entity.HasKey(p=> p.ProductNumber);
                entity.HasIndex(p => p.Name).IsUnique();

            }  //Bir nece key elediyimiz cod

        );
    }
}

//class FalanAttribute : CompareAttribute
//{
//    public FalanAttribute(string otherProperty) : base(otherProperty)
//    {

//    }

//}