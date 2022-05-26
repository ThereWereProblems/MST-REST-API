namespace MST_REST_Web_API.Entities
{
    public class Script
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public bool Succes { get; set; }
        public string Description { get; set; }
        public virtual List<Endpoint> Endpoints { get; set; }
        public string Comment { get; set; }
        public int TesterId { get; set; }
    }
}
