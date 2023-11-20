namespace WardrobeAPI.DTOs
{
    public class ApparelRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Tittle cannot be longer than 32 chars")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(256, ErrorMessage = "Description cannot be longer than 256 chars")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(16, ErrorMessage = "Color cannot be longer than 16 chars")]
        public string Color { get; set; } = string.Empty;

        [Required]
        public int ClosetId { get; set; }
    }

}
