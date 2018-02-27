using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChinookConsole.App
{
    internal class EmployeeModifier
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        string[] SeparateName(string name)
        {
            var newEmployeeName = name.Split(' ');
            return newEmployeeName;
        }

        public bool UpdateEmployeeName(int enteredEmployeeId, string enteredEmployeeName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();

                connection.Open();
                
                cmd.CommandText = @"Update Employee
                                   Set LastName = @lastName
                                      ,FirstName = @firstName  
                                 Where EmployeeId = @employeeID";

                var newName = SeparateName(enteredEmployeeName);


                var employeeIdToEdit = new SqlParameter("@employeeID", SqlDbType.Int);
                employeeIdToEdit.Value = enteredEmployeeId;
                cmd.Parameters.Add(employeeIdToEdit);

                var newFirstName = new SqlParameter("@firstName", SqlDbType.NVarChar);
                newFirstName.Value = newName[0];
                cmd.Parameters.Add(newFirstName);

                var newLastName = new SqlParameter("@lastName", SqlDbType.NVarChar);
                newLastName.Value = newName[1];
                cmd.Parameters.Add(newLastName);

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }
    }
}