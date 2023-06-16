using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
//Console.WriteLine("");
using (var db = new AppContext())
{
    var teacher = new Teacher() { TeacherName = "Orxan", Surname = "Orxanli", EmploymentDate = new DateTime(2000,1,1), Salary = 20, PremiumTeachers = 5 };
    db.teachers.Add(teacher);
    db.SaveChanges();
    var group = new Groups { GroupName = "Pro123", GroupRaiting = 2, GroupYear = 3 };
    db.Groups.Add(group);
    db.SaveChanges();

    var dep = new Departments { DepartmentName="Programs",Financing=5};
    db.departments.Add(dep);
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
    [DisplayFormat(DataFormatString = "0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
    public DateTime EmploymentDate { get; set; }
    [Required]
    public decimal Salary { get; set; }
    [Required]
    public decimal PremiumTeachers { get; set; }




}
[Index(nameof(FacultyName), IsUnique = true)]
public class Faculties
{
    [Key]
    public int Id { get; set; }
    [StringLength(100)]
    public string FacultyName { get; set; } = String.Empty;

}
[Index(nameof(DepartmentName), IsUnique = true)]
public class Departments
{
    [Key]
    public int Id { get; set; }
    [StringLength(100)]
    public string DepartmentName { get; set; } = String.Empty;
    public decimal Financing { get; set; }


}

[Index(nameof(GroupName), IsUnique = true)]
public class Groups
{
    [Key]
    public int Id { get; set; }
    [StringLength(10)]
    public string GroupName { get; set; }

    public int GroupRaiting { get; set; }
    public int GroupYear { get; set; }
}


public class AppContext : DbContext
{
    public DbSet<Teacher> teachers { get; set; }
    public DbSet<Faculties> faculty { get; set; }
    public DbSet<Departments> departments { get; set; }
    public DbSet<Groups> Groups { get; set; }

    public AppContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataAnnotations;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Groups>(group =>
        {
            group.Property(g => g.GroupYear).HasAnnotation("Range", new[] { 1, 5 });
            group.Property(d => d.GroupRaiting).HasAnnotation("Range", new[] { 0, 5 });

            modelBuilder.Entity<Groups>().HasCheckConstraint("CK_Groups_GroupYear_Range", "GroupYear >= 1 AND GroupYear <= 5")
            .HasCheckConstraint("CK_Groups_GroupRaiting_Range", "GroupRaiting >= 0 AND GroupRaiting <= 5");
        }
        );

        modelBuilder.Entity<Departments>().Property(d => d.Financing).HasDefaultValue(0);
        modelBuilder.Entity<Departments>().HasCheckConstraint("CK_Groups_Financing_Check", "Financing >= 0 ");

        modelBuilder.Entity<Teacher>().HasCheckConstraint("CK_EmploymentDate_Check", "EmploymentDate >= '1990-01-01' ")
            .HasCheckConstraint("CK_PremiumTeachers_Check", "PremiumTeachers >= 0 ")
            .HasCheckConstraint("CK_Salary_Check", "Salary > 0 ");
        modelBuilder.Entity<Teacher>().Property(p => p.PremiumTeachers).HasDefaultValue(0);
    }



}


