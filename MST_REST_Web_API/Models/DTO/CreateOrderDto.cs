namespace MST_REST_Web_API.Models.DTO
{
    public class CreateOrderDto
    {
        public List<int> ProductsIds { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
    }
}
