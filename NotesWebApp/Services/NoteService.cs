using Microsoft.EntityFrameworkCore;
using NotesWebApp.Data;
using NotesWebApp.Models;

namespace NotesWebApp.Services
{
    public class NoteService
    {
        private readonly ApplicationDbContext _context;
        public NoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }
        
        public async Task<List<Note>> GetNotesByPageAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var query = _context.Notes.AsQueryable();

            // Filtrera med söktermen om den inte är tom
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(n =>
                    n.Title.Contains(searchTerm) ||
                    n.ShortDescription.Contains(searchTerm));
            }

            // Använd query för paginering och sortering
            return await query
                           .OrderByDescending(n => n.CreatedDateTime) // Sortera nyaste först
                           .Skip((pageNumber - 1) * pageSize) // Hoppa över anteckningar för tidigare sidor
                           .Take(pageSize) // Ta det antal anteckningar som passar på sidan
                           .ToListAsync(); // Hämta resultatet som en lista
        }
        public async Task<int> GetTotalNotesCountAsync(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                return await _context.Notes
                    .CountAsync(n =>
                        n.Title.Contains(searchTerm) ||
                        n.ShortDescription.Contains(searchTerm) );
            }
            else
            {
                return await _context.Notes.CountAsync();
            }
        }
        public async Task<int> GetTotalNotesCountAsync()
        {
            return await _context.Notes.CountAsync();
        }

        public async Task<Note> GetNoteByIdAsync(int Id) 
        {
            return await _context.Notes.FindAsync(Id);
        }
        public async Task UpdateNoteAsync(Note note) 
        {
            var existingNote = await _context.Notes.FindAsync(note.Id);
            if (existingNote != null) 
            {
                existingNote.Title = note.Title;
                existingNote.ShortDescription = note.ShortDescription;
                existingNote.Notes = note.Notes;
                existingNote.CreatedDateTime = DateTime.Now;

                // Uppdatera bilddata
                if (note.ImageData.Any())
                {
                    existingNote.ImageData = note.ImageData; // Uppdatera med nya bilder
                }

                await _context.SaveChangesAsync();

            }
        }
        public async Task DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }


    }
}
