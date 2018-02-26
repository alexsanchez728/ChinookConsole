using ADOExample.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChinookConsole.App.DataAccess
{
    class AgentInvoiceQuery
    {

        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        public List<Invoice> GetInvoiceByEmployeeId(string EmployeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"Select e.Firstname + e.Lastname as Name, i.InvoiceId
	                                    From Employee e
                                            join customer c on c.SupportRepId = e.EmployeeId
                                                join invoice i on i.CustomerId = c.CustomerId
	                                    where e.Title='Sales Support Agent' OR e.Title='Sales Manager'";


                var reader = cmd.ExecuteReader();

                var invoices = new List<Invoice>();


                while (reader.Read())
                {
                    var invoice = new Invoice
                    {
                        InvoiceId = int.Parse(reader["InvoiceId"].ToString()),
                        SalesAgent = reader["Name"].ToString(),
                    };

                    invoices.Add(invoice);
                }

                return invoices;
            }
        }
    }
}
