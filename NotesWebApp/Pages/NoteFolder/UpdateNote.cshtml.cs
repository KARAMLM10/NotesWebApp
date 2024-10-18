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

            // Kontrollera om nya bilder har laddats upp
            if (ImageFiles.Any())
            {
                foreach (var imageFile in ImageFiles)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        var imageData = memoryStream.ToArray();
                        Note.ImageData.Add(imageData); // Lägg till varje ny bild i ImageData-listan
                    }
                }
            }
            await _noteService.UpdateNoteAsync(Note);
            return RedirectToPage("/index");
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            //if (Note.Id == 0)
            //{
            //    return BadRequest("Ogiltigt ID");
            //}

            //// Kontrollera om det finns nya bilder att ladda upp
            //if (ImageFiles.Any())
            //{
            //    var imagePaths = new List<string>();
            //    var imageDirectory = Path.Combine(_environment.WebRootPath, "images");

            //    // Skapa images-katalogen om den inte redan finns
            //    if (!Directory.Exists(imageDirectory))
            //    {
            //        Directory.CreateDirectory(imageDirectory);
            //    }

            //    // Ladda upp och spara varje ny bild
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


        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using NotesWebApp.Models;
//using NotesWebApp.Services;

//namespace NotesWebApp.Pages.NoteFolder
//{
//    public class UpdateNoteModel : PageModel
//    {
//        private readonly NoteService _noteService;
//        [BindProperty]
//        public Note Note { get; set; }
//        [BindProperty]
//        public IFormFile NewImageFile { get; set; } // Ny bild att ladda upp
//        public UpdateNoteModel(NoteService noteService)
//        {
//            _noteService = noteService;
//        }


//        public async Task<IActionResult> OnGetAsync(int Id)
//        {
//            Note = await _noteService.GetNoteByIdAsync(Id);
//            if (Note == null) 
//            {
//                return Page();
//            }
//            return Page();
//        }
//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }
//            if (Note.Id == 0)
//            {
//                return BadRequest("ogiltigt ID");
//            }

//            // Om en ny bild har valts, spara den som byte-array
//            if (NewImageFile != null)
//            {
//                using (var memoryStream = new MemoryStream())
//                {
//                    await NewImageFile.CopyToAsync(memoryStream);
//                    Note.ImageData = memoryStream.ToArray(); // Spara bilddata i databasen
//                    Note.ImageName = Path.GetFileName(NewImageFile.FileName); // Valfritt: spara filnamn
//                }
//            }
//            await _noteService.UpdateNoteAsync(Note);
//            return RedirectToPage("/index");

//        }




//    }
//}