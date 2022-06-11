namespace MST_REST_Web_API.Models.DTO
{
    public class ScriptDto
    {
        public string Name { get; set; }
        public List<MST_REST_Web_API.Models.DTO.EndpointDto> Endpoints { get; set; }
        public string Description { get; set; }
    }
}
