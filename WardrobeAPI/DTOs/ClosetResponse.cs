namespace WardrobeAPI.DTOs
{
    public class ClosetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<ClosetApparelResponse> Apparels { get; set; } = new List<ClosetApparelResponse>();
    }
    public class ClosetApparelResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
