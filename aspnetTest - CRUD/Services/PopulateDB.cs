using aspnetapp.Models;
using System.Linq;

namespace aspnetapp.Services
{
    public class PopulateDB
    {
        public static void Populate(SalesContext context)
        {
            //Created SalesPerson (Item 1B - Check)
            if (context.SalesPersonData.Count() == 0)
            {
                context.SalesPersonData.AddRange(new SalesPerson
                {
                    Name = "Charlie"
                }, new SalesPerson
                {
                    Name = "Doug"
                }
                );
            }                            
           
            if (context.SalesData.Count() == 0)
            {
                context.SalesData.AddRange(new Sales
                {
                    CustomerName = "Schneider Electric.",
                    Salesperson = "Charlie",
                    hasPayment = true,
                    Price = 199
                }, new Sales
                {
                    CustomerName = "Netflix",
                    Salesperson = "Doug",
                    hasPayment = false,
                    Price = 13
                });
            }

            context.SaveChanges();
        }
    }
}