using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
//Console.WriteLine("");
using(var db = new AppContext())
{
    var teacher = new Teacher() { TeacherName = "Orxan", Surname = "Orxanli", EmploymentDate = new DateTime(1000, 1, 1), Salary = 200, PremiumTeachers = 500 };
    db.teachers.Add(teacher);
    db.SaveChanges();
    
}


public class Teacher
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string TeacherName { get; set; } = String.Empty;
    [Required]
    public string Surname { get; set; } = String.Empty;
    [Required]
    [Range(typeof(DateTime), "01/01/1990", "12/31/2023", ErrorMessage = "Daxil etiyiniz Tarix 01.01.1990'dən kiçikdir")]
    public DateTime EmploymentDate { get; set; }
    [Required]
    [Range(typeof(decimal), "1", "79228162514264337593543950335", ErrorMessage = "0 dan kiçikdir ")]
    public decimal Salary { get; set; }
    [Required]
    [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "0 dan kiçikdir ")]
    public decimal PremiumTeachers { get; set; }




}

public class Faculties
{
    public int Id { get; set; }
    public string FacultyName { get; set; } = String.Empty;

}

public class Departments
{
    public int Id { get; set; }

    public string DepartmentName { get; set; } = String.Empty;
    public decimal Financing { get; set; }


}
public class Groups
{
    public int Id { get; set; }
    public string GroupName { get; set; }

    public int GroupRaiting { get; set; }
    public int GroupYear { get; set; }
}


public class AppContext : DbContext
{
    public DbSet<Teacher> teachers { get; set; }
    public DbSet<Faculties> faculty { get; set; }
    public DbSet<Departments> departments { get; set; }

    public AppContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataAnnotations;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }



}