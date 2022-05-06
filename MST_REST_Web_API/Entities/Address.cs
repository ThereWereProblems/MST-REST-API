using System.ComponentModel.DataAnnotations;

namespace MST_REST_Web_API.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Town { get; set; }
        public string Street { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
