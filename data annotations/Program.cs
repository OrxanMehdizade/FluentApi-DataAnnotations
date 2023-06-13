// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

public class Teacher
{
    public int Id { get; set; }
    public string TeacherName { get; set; }=String.Empty;
    public string Surname { get; set; }=String.Empty;
    public DateTime Date { get; set; }
    public float Salary { get; set; }




}

public class Faculty
{
    public int Id { get; set; }
    public string FacultyName { get; set; }=String.Empty;

}

public class Department
{
    public int Id { get; set; }

    public string DepartmentName { get; set; }=String.Empty;
}
public class Group
{
    public int Id { get; set; }
    public string GroupName { get; set; }

    public int GroupRaiting { get; set; }
}


public class AppContext : DbContext
{
    public DbSet<Teacher> teachers { get; set; }
    public DbSet<Faculty> faculty { get; set; }
    public DbSet<Department> departments { get; set; }

    public AppContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

   
}