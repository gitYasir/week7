using SpartaToDo.Models;
using SpartaToDo.Models.ViewModels;

namespace SpartaToDo.Controllers {
    public static class Utils {
        public static ToDo GenerateToDoFromCreateToDoViewModel(
            CreateToDoViewModel viewModel, Spartan user ) =>
                new ToDo {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Complete = viewModel.Complete,
                    SpartanId = user.Id,
                    Spartan = user
                };

        public static ToDoViewModel GenerateToDoViewModel( ToDo todo ) =>
        new ToDoViewModel { ToDo = todo };

        public static ToDo GenerateToDoFromViewModel( ToDoViewModel viewModel, ToDo toDoFromDb ) {
            toDoFromDb.Title = viewModel.ToDo.Title;
            toDoFromDb.Description = viewModel.ToDo.Description;
            toDoFromDb.Complete = viewModel.ToDo.Complete;
            return toDoFromDb;
        }
    }
}