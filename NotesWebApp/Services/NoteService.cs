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
        public async Task<List<Note>> GetNotesByPageAsync(int pageNumber, int pageSize)
        {
            return await _context.Notes
                           .OrderByDescending(n => n.CreatedDateTime) // Sortera senaste först
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .ToListAsync();
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
                existingNote.ImagePaths = note.ImagePaths;

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
