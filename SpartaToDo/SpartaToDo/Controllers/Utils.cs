using SpartaToDo.Models;
using SpartaToDo.Models.ViewModels;

namespace SpartaToDo.Controllers {
    public static class Utils {
        public static ToDo GenerateToDoFromCreateToDoViewModel(
            CreateToDoViewModel viewModel ) =>
                new ToDo {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Complete = viewModel.Complete
                };

        public static ToDoViewModel GenerateToDoViewModel( ToDo todo ) =>
        new ToDoViewModel { ToDo = todo };

        public static ToDo GenerateToDoFromViewModel( ToDoViewModel model ) =>
        model.ToDo;
    }
}