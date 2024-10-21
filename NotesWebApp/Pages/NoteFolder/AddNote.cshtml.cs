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

                    // Spara bilder som byte-arrayer i ett nytt fält
                    Note.ImageData = imageByteArrays;
                }

                // Sätt CreatedDateTime till nuvarande datum och tid
                Note.CreatedDateTime = DateTime.Now;

                await _noteService.AddNoteAsync(Note);
                TempData["SuccessMessage"] = "Note Successfully Saved";
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "The note could not be saved. Try again.");
                Console.WriteLine(ex.ToString());
                return Page();
            }



        }

    }
}
