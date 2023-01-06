namespace SpartaToDo.Models.ViewModels {
    public class ToDoViewModel {
        public string? PageTitle { get; set; }
        public ToDo ToDo { get; set; } = null!;
    }
}
