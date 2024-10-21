using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotesWebApp.Models;
using NotesWebApp.Services;

namespace NotesWebApp.Pages.NoteFolder
{
    public class UpdateNoteModel : PageModel
    {
        private readonly NoteService _noteService;
        private readonly IWebHostEnvironment _environment;  // Miljö för att hantera filvägar
        [BindProperty]
        public Note Note { get; set; }
        [BindProperty]
        public List<IFormFile> ImageFiles { get; set; } = new();  // Hantera flera bilder

        public UpdateNoteModel(NoteService noteService, IWebHostEnvironment environment)
        {
            _noteService = noteService;
            _environment = environment;
        }

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            Note = await _noteService.GetNoteByIdAsync(Id);
            if (Note == null)
            {
                return Page();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Note.Id == 0)
            {
                return BadRequest("Ogiltigt ID");
            }

            // Uppdatera textinnehållet
            var existingNote = await _noteService.GetNoteByIdAsync(Note.Id);
            if (existingNote != null)
            {
                existingNote.Title = Note.Title;
                existingNote.ShortDescription = Note.ShortDescription;
                existingNote.Notes = Note.Notes; // Spara anteckningens text

                // Kontrollera om nya bilder har laddats upp
                if (ImageFiles.Any())
                {
                    foreach (var imageFile in ImageFiles)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageFile.CopyToAsync(memoryStream);
                            var imageData = memoryStream.ToArray();
                            existingNote.ImageData.Add(imageData); // Lägg till varje ny bild i ImageData-listan
                        }
                    }
                }

                await _noteService.UpdateNoteAsync(existingNote);
            }

            return RedirectToPage("/index");




        }
    }
}

//if (!ModelState.IsValid)
//{
//    return Page();
//}

//if (Note.Id == 0)
//{
//    return BadRequest("Ogiltigt ID");
//}

//// Kontrollera om nya bilder har laddats upp
//if (ImageFiles.Any())
//{
//    foreach (var imageFile in ImageFiles)
//    {
//        using (var memoryStream = new MemoryStream())
//        {
//            await imageFile.CopyToAsync(memoryStream);
//            var imageData = memoryStream.ToArray();
//            Note.ImageData.Add(imageData); // Lägg till varje ny bild i ImageData-listan
//        }
//    }
//}
//await _noteService.UpdateNoteAsync(Note);
//return RedirectToPage("/index");