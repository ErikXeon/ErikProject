using ErikProject;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class DatabaseHelper
{
    private string connectionString = "Server=localhost;Database=VacationManagement;Uid=root;Pwd=root;";

    public List<Employee> GetAllWorkers()
    {
        var workers = new List<Employee>();

        using (var connect = new MySqlConnection(connectionString))
        {
            connect.Open();
            var sql = new MySqlCommand("SELECT * FROM Employees", connect);
            var result = sql.ExecuteReader();

            while (result.Read())
            {
                workers.Add(new Employee
                {
                    EmployeeID = (int)result["EmployeeID"],
                    FullName = result["FullName"].ToString(),
                    Position = result["Position"].ToString(),
                    Department = result["Department"].ToString()
                });
            }
        }
        return workers;
    }

    public bool AddWorker(Employee worker)
    {
        using (var connect = new MySqlConnection(connectionString))
        {
            connect.Open();
            var sql = new MySqlCommand(
                "INSERT INTO Employees (FullName, Position, Department) VALUES (@name, @job, @depart)",
                connect);

            sql.Parameters.AddWithValue("@name", worker.FullName);
            sql.Parameters.AddWithValue("@job", worker.Position);
            sql.Parameters.AddWithValue("@depart", worker.Department);

            return sql.ExecuteNonQuery() > 0;
        }
    }

    public bool UpdateWorker(Employee worker)
    {
        using (var connect = new MySqlConnection(connectionString))
        {
            connect.Open();
            var sql = new MySqlCommand(
                "UPDATE Employees SET FullName=@name, Position=@job, Department=@depart WHERE EmployeeID=@id",
                connect);

            sql.Parameters.AddWithValue("@id", worker.EmployeeID);
            sql.Parameters.AddWithValue("@name", worker.FullName);
            sql.Parameters.AddWithValue("@job", worker.Position);
            sql.Parameters.AddWithValue("@depart", worker.Department);

            return sql.ExecuteNonQuery() > 0;
        }
    }

    public bool DeleteWorker(int workerId)
    {
        using (var connect = new MySqlConnection(connectionString))
        {
            connect.Open();
            var sql = new MySqlCommand("DELETE FROM Employees WHERE EmployeeID=@id", connect);
            sql.Parameters.AddWithValue("@id", workerId);
            return sql.ExecuteNonQuery() > 0;
        }
    }

    public List<Vacation> GetAllVacations()
    {
        var vacations = new List<Vacation>();

        using (var connect = new MySqlConnection(connectionString))
        {
            connect.Open();
            var sql = new MySqlCommand(
                "SELECT v.*, e.FullName FROM Vacations v JOIN Employees e ON v.EmployeeID = e.EmployeeID",
                connect);

            var result = sql.ExecuteReader();

            while (result.Read())
            {
                vacations.Add(new Vacation
                {
                    VacationID = (int)result["VacationID"],
                    EmployeeID = (int)result["EmployeeID"],
                    StartDate = (DateTime)result["StartDate"],
                    EndDate = (DateTime)result["EndDate"],
                    VacationType = result["VacationType"].ToString(),
                    EmployeeName = result["FullName"].ToString()
                });
            }
        }
        return vacations;
    }

    public bool AddVacation(Vacation vac)
    {
        using (var connect = new MySqlConnection(connectionString))
        {
            connect.Open();
            var sql = new MySqlCommand(
                "INSERT INTO Vacations (EmployeeID, StartDate, EndDate, VacationType) VALUES (@workerId, @start, @end, @type)",
                connect);

            sql.Parameters.AddWithValue("@workerId", vac.EmployeeID);
            sql.Parameters.AddWithValue("@start", vac.StartDate);
            sql.Parameters.AddWithValue("@end", vac.EndDate);
            sql.Parameters.AddWithValue("@type", vac.VacationType);

            return sql.ExecuteNonQuery() > 0;
        }
    }

    public bool UpdateVacation(Vacation vac)
    {
        using (var connect = new MySqlConnection(connectionString))
        {
            connect.Open();
            var sql = new MySqlCommand(
                "UPDATE Vacations SET EmployeeID=@workerId, StartDate=@start, EndDate=@end, VacationType=@type WHERE VacationID=@id",
                connect);

            sql.Parameters.AddWithValue("@id", vac.VacationID);
            sql.Parameters.AddWithValue("@workerId", vac.EmployeeID);
            sql.Parameters.AddWithValue("@start", vac.StartDate);
            sql.Parameters.AddWithValue("@end", vac.EndDate);
            sql.Parameters.AddWithValue("@type", vac.VacationType);

            return sql.ExecuteNonQuery() > 0;
        }
    }

    public bool DeleteVacation(int vacationId)
    {
        using (var connect = new MySqlConnection(connectionString))
        {
            connect.Open();
            var sql = new MySqlCommand("DELETE FROM Vacations WHERE VacationID=@id", connect);
            sql.Parameters.AddWithValue("@id", vacationId);
            return sql.ExecuteNonQuery() > 0;
        }
    }
}