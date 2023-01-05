using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpartaToDo.Models;

namespace SpartaToDo.Data {
    public class SpartaToDoContext : IdentityDbContext {
        public SpartaToDoContext( DbContextOptions<SpartaToDoContext> options )
            : base( options ) {
        }
        public DbSet<ToDo> ToDos { get; set; }
    }
}