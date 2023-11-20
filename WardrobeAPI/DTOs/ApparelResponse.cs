namespace WardrobeAPI.DTOs
{
    public class ApparelResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int ClosetId { get; set; }
    }
}
