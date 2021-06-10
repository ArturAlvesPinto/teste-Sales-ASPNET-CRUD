using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


//Creation Class SalesPerson (Item 1A - Check)
namespace aspnetapp.Models
{
    public class SalesPerson
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        
    }
}