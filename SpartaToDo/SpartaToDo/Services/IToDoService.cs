using SpartaToDo.Models;

namespace SpartaToDo.Services {
    public interface IToDoService {
        Task<List<ToDo>> GetAllAsync();
        Task<ToDo?> FindOneAsync( int? id );
        Task AddAsync( ToDo toDo );
        Task DeleteAsync( ToDo toDo );
        Task SaveChangesAsync();
        Task UpdateAsync( ToDo toDo );
        bool NoToDos();
        bool Exists( int id );
    }
}
