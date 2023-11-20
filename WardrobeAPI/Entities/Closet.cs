namespace WardrobeAPI.Entities
{
    public class Closet
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Name { get; set; }

        public List<Apparel> Apparels { get; set; } = new();
    }
}
