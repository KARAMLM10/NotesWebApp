namespace NotesWebApp.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ShortDescription { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public string? Notes { get; set; }
        public string? ImagePaths { get; set; } // Ändra till en enda sträng och spara bildvägarna som en kommaseparerad sträng
        public List<byte[]> ImageData { get; set; } = new();

        //public List<string>? ImagePaths { get; set; }
        // Lagra bilden som en byte-array i databasen
        //public byte[]? Image { get; set; }

        //// Lagra bildens sökväg om du också vill spara den
        //public string? ImagePath { get; set; }

    }
}
