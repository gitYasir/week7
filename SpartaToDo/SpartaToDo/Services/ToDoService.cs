using Microsoft.EntityFrameworkCore;
using SpartaToDo.Data;
using SpartaToDo.Models;

namespace SpartaToDo.Services {
    public class ToDoService : IToDoService {
        private readonly SpartaToDoContext _context;

        public ToDoService( SpartaToDoContext context ) {
            _context = context;
        }

        public async Task AddAsync( ToDo toDo ) {
            _context.ToDos.Add( toDo );
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync( ToDo toDo ) {
            _context.ToDos.Remove( toDo );
            await _context.SaveChangesAsync();
        }

        public bool Exists( int id ) {
            return ( _context.ToDos?.Any( e => e.Id == id ) ).GetValueOrDefault();
        }

        public Task<ToDo?> FindOneAsync( int? id ) {
            return _context.ToDos
                .FirstOrDefaultAsync( m => m.Id == id );
        }

        public async Task<List<ToDo>> GetAllAsync() {
            return await _context.ToDos.ToListAsync();
        }

        public bool NoToDos() {
            return _context.ToDos == null;
        }

        public async Task SaveChangesAsync() {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync( ToDo toDo ) {
            _context.Update( toDo );
            await _context.SaveChangesAsync();
        }

        public async Task<List<ToDo>> GetListBySpartanIdAsync( string id ) {
            return await _context.ToDos.Where( td => td.SpartanId == id ).ToListAsync();
        }
    }
}
