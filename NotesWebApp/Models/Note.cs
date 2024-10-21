namespace NotesWebApp.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ShortDescription { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public string? Notes { get; set; }
        public List<byte[]> ImageData { get; set; } = new();

    }
}
