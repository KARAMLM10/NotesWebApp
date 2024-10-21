using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotesWebApp.Data;
using NotesWebApp.Models;
using NotesWebApp.Services;

namespace NotesWebApp.Pages.NoteFolder
{
    public class ReadALLNotesModel : PageModel
    {
        private readonly NoteService _noteService;
        public List<Note> Notes { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        [TempData]
        public string Message { get; set; }

        public ReadALLNotesModel(NoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 5;
            Notes = await _noteService.GetNotesByPageAsync(pageNumber, pageSize);
            int totalNotes = await _noteService.GetTotalNotesCountAsync();
            TotalPages = (int)Math.Ceiling(totalNotes / (double)pageSize);
            CurrentPage = pageNumber;
            //Notes = await _noteService.GetALLNotesAsync();
        }
        public async Task<IActionResult> OnPostDeleteNoteAsync(int id)
        {
            await _noteService.DeleteNoteAsync(id);
            Message = "The note has been deleted."; // Meddelande som kan visas för användaren
            return RedirectToPage(); // Återvänd till samma sida för att uppdatera listan
        }



    }
}
