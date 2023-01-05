using System.ComponentModel.DataAnnotations;

namespace SpartaToDo.Models {
    public class ToDo {
        public int Id { get; set; }

        [Required( ErrorMessage = "Title is Required" )]
        [StringLength( 50 )]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Display( Name = "Complete?" )]
        public bool Complete { get; set; }

        [DataType( DataType.Date )]
        [Display( Name = "Created" )]
        public DateTime DateCreated { get; init; } = DateTime.Now;
    }
}