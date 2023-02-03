namespace Demo.Models;

public class Employee
{
    public int EmployeeID { get; set; }
    public string Name { get; set; }
    public override string ToString() => Name;

}