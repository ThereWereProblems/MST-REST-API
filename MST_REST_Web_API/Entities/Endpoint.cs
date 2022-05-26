namespace MST_REST_Web_API.Entities
{
    public class Endpoint
    {
        public int Id { get; set; }
        public int EndpointTypeId { get; set; }
        public virtual EndpointType EndpointType { get; set; }
        public string Body { get; set; }
        public string Heder { get; set; }
        public string Parametrs { get; set; }
        public string URL { get; set; }
    }
}
