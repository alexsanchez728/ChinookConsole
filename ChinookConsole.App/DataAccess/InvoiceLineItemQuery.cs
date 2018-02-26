using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChinookConsole.App.DataAccess
{
    class InvoiceLineItemQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        public int GetInvoiceLineItems(int inputId)
        {
            using (var Connection = new SqlConnection(_connectionString))
            {
                Connection.Open();
                var cmd = Connection.CreateCommand();
                cmd.CommandText = @"Select count(*) as [CountOfInvoiceLineItems]
                                        from InvoiceLine
                                    where  InvoiceId = @invoiceId";

                var invoiceId = new SqlParameter("@invoiceId", SqlDbType.Int);
                invoiceId.Value = inputId;
                cmd.Parameters.Add(invoiceId);

                var CountOfTotalItems = (int.Parse(cmd.ExecuteScalar().ToString()));
                return CountOfTotalItems;
            }
        }
    }
}
