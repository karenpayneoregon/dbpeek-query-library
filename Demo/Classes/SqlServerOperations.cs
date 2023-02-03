using Demo.Models;
using System.Data;
using System.Data.SqlClient;
using DbPeekQueryLibrary.LanguageExtensions;
using Serilog;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace Demo.Classes;
internal class SqlServerOperations
{
    /// <summary>
    /// Example where <see cref="countryName"/> is not correct
    /// </summary>
    /// <param name="countryName">Country name</param>
    public static List<DataContainer> Example1(string countryName)
    {
        List<DataContainer> list = new();
        
        using SqlConnection cn = new(ConnectionString());
        using SqlCommand cmd = new() { Connection = cn, CommandText = Statement1 };
        
        cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = countryName;

        if (ApplicationSettings.Instance.LogSqlCommands)
        {
            Log.Information($"{nameof(SqlServerOperations)}.{nameof(Example1)}\n{cmd.ActualCommandText()}");
        }

        cn.Open();

        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new DataContainer()
            {
                CustomerIdentifier = reader.GetInt32(0),
                CompanyName = reader.GetString(1),
                ContactTitle = reader.GetString(2),
                FirstName = reader.GetString(3),
                LastName = reader.GetString(4),
                ContactIdentifier = reader.GetInt32(5),
                ContactTypeIdentifier = reader.GetInt32(6),
                CountryIdentfier = reader.GetInt32(7)
            });
        }

        return list;

    }

    /// <summary>
    /// Example where <see cref="orderId"/> does not exist
    /// </summary>
    /// <param name="orderId">order identifier</param>
    public static (Employee employee, bool success) Example2(int orderId)
    {
        using SqlConnection cn = new(ConnectionString());
        using SqlCommand cmd = new() { Connection = cn, CommandText = Statement2 };

        cmd.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;

        if (ApplicationSettings.Instance.LogSqlCommands)
        {
            Log.Information($"{nameof(SqlServerOperations)}.{nameof(Example2)}\n{cmd.ActualCommandText()}");
        }

        cn.Open();

        var reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            reader.Read();

            return (new Employee() { EmployeeID = reader.GetInt32(0), Name = reader.GetString(1) }, true);

        }
        else
        {
            return (null, false);
        }

    }

    /// <summary>
    /// For <see cref="Example1"/>
    /// </summary>
    public static string Statement1 =>
        """
        SELECT C.CustomerIdentifier,
               C.CompanyName,
               CT.ContactTitle,
               Contact.FirstName,
               Contact.LastName,
               C.ContactIdentifier,
               C.ContactTypeIdentifier,
               C.CountryIdentfier
        FROM dbo.Customers AS C
            INNER JOIN dbo.Countries
                ON C.CountryIdentfier = id
            INNER JOIN dbo.Contact AS Contact
                ON C.ContactIdentifier = Contact.ContactIdentifier
            INNER JOIN dbo.ContactType AS CT
                ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
        WHERE (CountryName = @CountryName);
        """;

    /// <summary>
    /// For <see cref="Example2"/>
    /// </summary>
    public static string Statement2 =>
        """
        SELECT O.EmployeeID,
               E.FirstName + ' ' + E.LastName AS 'EmpName'
        FROM dbo.Orders AS O
            INNER JOIN dbo.Employees AS E
                ON O.EmployeeID = E.EmployeeID
        WHERE O.OrderID = @OrderId
        """;
}