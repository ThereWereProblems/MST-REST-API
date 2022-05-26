namespace MST_REST_Web_API.Models.DTO
{
    public class ScriptDto
    {
        public string Name { get; set; }
        public List<MST_REST_Web_API.Entities.Endpoint> Endpoints { get; set; }
        public string Description { get; set; }
    }
}
