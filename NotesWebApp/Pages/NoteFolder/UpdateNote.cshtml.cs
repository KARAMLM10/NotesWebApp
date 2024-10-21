using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotesWebApp.Models;
using NotesWebApp.Services;

namespace NotesWebApp.Pages.NoteFolder
{
    public class UpdateNoteModel : PageModel
    {
        private readonly NoteService _noteService;
        private readonly IWebHostEnvironment _environment;  // Milj� f�r att hantera filv�gar
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

            // H�mta den befintliga anteckningen fr�n databasen
            var existingNote = await _noteService.GetNoteByIdAsync(Note.Id);
            if (existingNote != null)
            {
                existingNote.Title = Note.Title;
                existingNote.ShortDescription = Note.ShortDescription;
                existingNote.Notes = Note.Notes; // Uppdatera anteckningens text

                // Hantera uppdatering av bilder
                var newImageData = new List<byte[]>();

                // Om anv�ndaren har laddat upp nya bilder, l�gg till dem
                if (ImageFiles.Any())
                {
                    foreach (var imageFile in ImageFiles)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageFile.CopyToAsync(memoryStream);
                            var imageData = memoryStream.ToArray();
                            newImageData.Add(imageData); // L�gg till ny bilddata i listan
                        }
                    }
                }

                // J�mf�r befintliga bilder med de nya och ta bort de som inte l�ngre finns kvar
                var imagesToRemove = existingNote.ImageData
                    .Where(oldImage => !newImageData.Any(newImage => newImage.SequenceEqual(oldImage)))
                    .ToList();

                foreach (var image in imagesToRemove)
                {
                    existingNote.ImageData.Remove(image); // Ta bort bilder som inte l�ngre finns med
                }

                // L�gg till alla nya bilder som anv�ndaren har laddat upp
                existingNote.ImageData.AddRange(newImageData);

                await _noteService.UpdateNoteAsync(existingNote);
            }

            return RedirectToPage("/index");
        }

    }
}
