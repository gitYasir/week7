namespace SpartaToDo.Models.ViewModels {
    public class CreateToDoViewModel {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool Complete { get; set; }

        public string? PageTitle { get; set; }
    }
}