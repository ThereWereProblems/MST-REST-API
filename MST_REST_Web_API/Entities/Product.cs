using System.ComponentModel.DataAnnotations;

namespace MST_REST_Web_API.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string PhotoURL { get; set; }
    }
}
