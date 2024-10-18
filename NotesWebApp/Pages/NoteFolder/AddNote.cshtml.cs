using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotesWebApp.Models;
using NotesWebApp.Services;
using static System.Net.Mime.MediaTypeNames;

namespace NotesWebApp.Pages.NoteFolder
{
    public class AddNoteModel : PageModel
    {
        private readonly NoteService _noteService;
        private readonly IWebHostEnvironment _environment;  // Miljö för att hantera filvägar
        [BindProperty]
        public Note? Note { get; set; }
        [BindProperty]
        public List<IFormFile> ImageFiles { get; set; } = new();  // Hanterar flera bilder

        public AddNoteModel(NoteService noteService, IWebHostEnvironment environment)
        {
            _noteService = noteService;
            _environment = environment;
        }

        public async void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {

               

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                // Kontrollera om det finns bilder att ladda upp
                if (ImageFiles.Any())
                {
                    var imageByteArrays = new List<byte[]>();

                    foreach (var imageFile in ImageFiles)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageFile.CopyToAsync(memoryStream);
                            imageByteArrays.Add(memoryStream.ToArray());
                        }
                    }

                    // Spara bilder som byte-arrayer i ett nytt fält (ex: ImageData eller i en relaterad tabell)
                    Note.ImageData = imageByteArrays; // Du behöver uppdatera modellen för detta fält.
                }



                // Sätt CreatedDateTime till nuvarande datum och tid
                Note.CreatedDateTime = DateTime.Now;

                await _noteService.AddNoteAsync(Note);
                TempData["SuccessMessage"] = "Note Successfuly Saved";
                return Page();

            }
            catch (Exception ex)
            {// Logga undantaget
                ModelState.AddModelError(string.Empty, "Det gick inte att spara anteckningen. Försök igen.");
                Console.WriteLine(ex.ToString());
                return Page();
            }


        }
    }
}

//if (!ModelState.IsValid)
//{
//    return Page();
//}

//// Kontrollera om det finns bilder att ladda upp
//if (ImageFiles.Any())
//{
//    foreach (var imageFile in ImageFiles)
//    {
//        using (var memoryStream = new MemoryStream())
//        {
//            await imageFile.CopyToAsync(memoryStream);
//            var base64Image = Convert.ToBase64String(memoryStream.ToArray());
//            var imageTag = $"<img src='data:image/png;base64,{base64Image}' />"; // Anpassa MIME-typ om det behövs
//            Note.Notes += imageTag; // Lägg till bilden till anteckningarna
//        }
//    }

//    var imagePaths = new List<string>();
//    var imageDirectory = Path.Combine(_environment.WebRootPath, "images");

//    // Skapa images-katalogen om den inte redan finns
//    if (!Directory.Exists(imageDirectory))
//    {
//        Directory.CreateDirectory(imageDirectory);
//    }

//    // Ladda upp och spara varje bild
//    foreach (var imageFile in ImageFiles)
//    {
//        var fileName = Path.GetFileName(imageFile.FileName);
//        var filePath = Path.Combine(imageDirectory, fileName);

//        using (var stream = new FileStream(filePath, FileMode.Create))
//        {
//            await imageFile.CopyToAsync(stream);
//        }

//        // Spara bildens relativa väg
//        imagePaths.Add($"/images/{fileName}");
//    }

//    // Spara bildvägar som en kommaseparerad sträng
//    Note.ImagePaths = string.Join(",", imagePaths);
//}