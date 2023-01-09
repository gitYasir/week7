using Microsoft.AspNetCore.Identity;

namespace SpartaToDo.Models {
    public class Spartan : IdentityUser {
        public List<ToDo>? ToDos { get; set; }
    }
}