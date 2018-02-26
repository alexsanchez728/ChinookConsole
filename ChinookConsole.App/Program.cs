using ChinookConsole.App.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cki = System.ConsoleKeyInfo;

namespace ChinookConsole.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var run = true;
            while (run)
            {
                cki userInput = MainMenu();

                switch (userInput.KeyChar)
                {
                    case '0':
                        run = false;
                        break;

                    case '1':
                        Console.Clear();
                        Console.WriteLine("Enter in an Employee ID # (1-5)");
                        var invoiceQuery = new AgentInvoiceQuery();

                        var employeeId = Console.ReadLine();

                        var agentInvoiceQuery = new AgentInvoiceQuery();
                        var agentsInvoices = agentInvoiceQuery.GetInvoiceByEmployeeId(employeeId);

                        foreach (var invoice in agentsInvoices)
                        {
                            Console.WriteLine($"Sales Agent: {invoice.SalesAgent} --- Invoices Id: {invoice.InvoiceId}");
                        }
                        Console.WriteLine("press enter to continue.");
                        Console.ReadLine();
                        break;

                    case '2':
                        Console.Clear();

                        employeeId = Console.ReadLine();

                        var allInvoicesQuery = new AgentInvoiceQuery();
                        agentsInvoices = allInvoicesQuery.GetInvoiceData();

                        foreach (var invoice in agentsInvoices)
                        {
                            Console.WriteLine($"Sales Agent: {invoice.SalesAgent}" +
                                $" Customer: {invoice.CustomerName}" +
                                $" Total: {invoice.Total}" +
                                $" Billing Country: {invoice.BillingCountry}");
                        }
                        Console.WriteLine("press enter to continue.");
                        Console.ReadLine();
                        break;
                }

                /* Show user Main Menu with 5 options
                1. Provide a query that shows the invoices associated with each sales agent. 
                    --The resultant table should include the Sales Agent's full name.
                2. Provide a query that shows the Invoice Total, Customer name, Country and Sale Agent name for all invoices
                3. Looking at the InvoiceLine table, provide a query that COUNTs the number of line items for an Invoice 
                    --with a parameterized Id from user input
                4. INSERT a new invoice with parameters for customerid and billing address
                5.UPDATE an Employee's name with a parameter for Employee Id and new name
                */

                // Given the user wants to see an agents invoices
                // When the select 1.
                // Then they should see all Agents by index to select



                // When an agent is selected
                // Then show that agent's name at the head of the console...
                // And Then show all invoice information on screen

                // Give the user a return to Agents option
                // Give the user a return to main menu option

            }


            cki MainMenu()
            {
                View mainMenu = new View()
                        .AddMenuOption("Show invoices by sales agent ID.")
                        .AddMenuOption("Show ALL the invoice data");
                Console.Write(mainMenu.GetFullMenu());
                cki userOption = Console.ReadKey();
                return userOption;
            }

        }
    }
}
