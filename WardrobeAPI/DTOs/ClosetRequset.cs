namespace WardrobeAPI.DTOs
{
    public class ClosetRequset
    {
        [Required]
        [StringLength(32, ErrorMessage = "Tittle cannot be longer than 32 chars")]
        public string Name { get; set; } = string.Empty;
    }
}
