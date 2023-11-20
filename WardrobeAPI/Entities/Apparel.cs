namespace WardrobeAPI.Entities
{
    public class Apparel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string Description { get; set; }
       
        [Column(TypeName = "nvarchar(16)")]
        public string Color { get; set; }
        
        [ForeignKey("Closet.Id")]
        public int ClosetId { get; set; }

        public Closet Closet { get; set; }

    }
}
