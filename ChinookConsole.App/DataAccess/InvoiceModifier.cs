using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookConsole.App.DataAccess
{
    class InvoiceModifier
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;
        
        public bool AddInvoice(int enteredCustomerId, String enteredBillingAddress)
        {
        using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Invoice
                                    (InvoiceId
                                   ,CustomerId
                                   ,InvoiceDate
                                   ,BillingAddress
                                   ,BillingCity
                                   ,BillingState
                                   ,BillingCountry
                                   ,BillingPostalCode
                                   ,Total)
                                   VALUES
                                   (@invoiceId,
                                    @customerId,
                                    2/26/2018,
                                    @billingAddress,
                                    @billingCity,
                                    @billingState,
                                    'USA',
                                    @billingZip,
                                    0)";

                connection.Open();

                var invoiceQuery = new AgentInvoiceQuery();

                var invoiceId = invoiceQuery.GetLastInvoice() + 1;

                var customerId = new SqlParameter("@customerId", SqlDbType.Int);
                customerId.Value = enteredCustomerId;
                cmd.Parameters.Add(customerId);

                var billingAddress = new SqlParameter("@billingAddress", SqlDbType.NVarChar);
                var billingValue = enteredBillingAddress;
                billingAddress.Value = billingValue;
                cmd.Parameters.Add(billingAddress);

                var result = cmd.ExecuteNonQuery();

                return result == 1;

            }
        }
    }
}
