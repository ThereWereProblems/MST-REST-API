using System.ComponentModel.DataAnnotations;

namespace MST_REST_Web_API.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalCost { get; set; }
    }
}
