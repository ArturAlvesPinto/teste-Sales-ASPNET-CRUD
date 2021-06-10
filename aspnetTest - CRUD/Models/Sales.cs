using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;



namespace aspnetapp.Models
{
    public class Sales
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Salesperson { get; set; }

        [Required]
        public float Price { get; set;}
        [Required]
        public bool hasPayment { get; set; }

    }
}