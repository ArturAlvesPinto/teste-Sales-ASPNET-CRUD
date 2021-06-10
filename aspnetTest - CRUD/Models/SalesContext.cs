using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace aspnetapp.Models
{
    public class SalesContext : DbContext
    {

        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
            
        }
        public DbSet<Sales> SalesData { get; set; }

        public DbSet<SalesPerson> SalesPersonData { get; set; }
    }
    
}