using SpartaToDo.Models;

namespace SpartaToDo.Data {
    public class SeedData {
        public static void Initialise( IServiceProvider service ) {
            var context = service.GetRequiredService<SpartaToDoContext>();

            if ( context.ToDos.Any() ) {
                context.ToDos.RemoveRange( context.ToDos.ToList() );
            }

            context.ToDos.AddRange(
                new ToDo {
                    Title = "Complete survey",
                    Description = "Complete the weekly survey feedback forms",
                    Complete = false,
                    DateCreated = new DateTime( 2023, 01, 03 )
                },
                new ToDo {
                    Title = "Timecards",
                    Description = "Complete timecard for this week",
                    Complete = true,
                    DateCreated = new DateTime( 2023, 01, 05 )
                },
                new ToDo {
                    Title = "Friday stand-up",
                    Description = "Do the academy stand-up on Friday for the group",
                    Complete = false,
                    DateCreated = new DateTime( 2023, 01, 03 )
                }
            );
            context.SaveChanges();
        }
    }
}