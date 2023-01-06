using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpartaToDo.Data;
using SpartaToDo.Models;
using SpartaToDo.Models.ViewModels;

namespace SpartaToDo.Controllers {
    public class ToDoController : Controller {
        private readonly SpartaToDoContext _context;

        public ToDoController( SpartaToDoContext context ) {
            _context = context;
        }

        // GET: ToDo
        public async Task<IActionResult> Index( string filter = "" ) {
            if ( _context.ToDos == null ) {
                return Problem( "There are no ToDos to do!" );
            }
            if ( string.IsNullOrEmpty( filter ) ) {
                return View( await _context.ToDos.ToListAsync() );
            }

            return View(
                ( await _context.ToDos.ToListAsync() )
                    .Where( td =>
                        td.Title.Contains(
                            filter, StringComparison.OrdinalIgnoreCase ) ||
                        td.Description!.Contains(
                            filter, StringComparison.OrdinalIgnoreCase )
                        ) );
        }

        // GET: ToDo/Details/5
        public async Task<IActionResult> Details( int? id ) {
            if ( id == null || _context.ToDos == null ) {
                return NotFound();
            }

            var toDo = await _context.ToDos
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( toDo == null ) {
                return NotFound();
            }

            return View( toDo );
        }

        // GET: ToDo/Create
        public IActionResult Create() {
            return View();
        }

        // POST: ToDo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateToDoViewModel model ) {
            var toDo = Utils.GenerateToDoFromCreateToDoViewModel( model );

            if ( TryValidateModel( toDo ) ) {
                _context.Add( toDo );
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
            }
            model.PageTitle = "Create";
            return View( model );
        }

        // GET: ToDo/Edit/5
        public async Task<IActionResult> Edit( int? id ) {
            if ( id == null || _context.ToDos == null ) {
                return NotFound();
            }

            var toDo = await _context.ToDos.FindAsync( id );
            if ( toDo == null ) {
                return NotFound();
            }
            ToDoViewModel viewModel = Utils.GenerateToDoViewModel( toDo );
            viewModel.PageTitle = "Edit";
            return View( viewModel );
        }

        // POST: ToDo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, ToDoViewModel viewModel ) {
            ToDo toDo = Utils.GenerateToDoFromViewModel( viewModel );

            if ( id != toDo.Id ) {
                return NotFound();
            }

            if ( TryValidateModel( toDo ) ) {
                try {
                    _context.Update( toDo );
                    await _context.SaveChangesAsync();
                }
                catch ( DbUpdateConcurrencyException ) {
                    if ( !ToDoExists( toDo.Id ) ) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction( nameof( Index ) );
            }
            return View( viewModel );
        }

        // GET: ToDo/Delete/5
        public async Task<IActionResult> Delete( int? id ) {
            if ( id == null || _context.ToDos == null ) {
                return NotFound();
            }

            var toDo = await _context.ToDos
                .FirstOrDefaultAsync( m => m.Id == id );
            if ( toDo == null ) {
                return NotFound();
            }

            return View( toDo );
        }

        // POST: ToDo/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed( int id ) {
            if ( _context.ToDos == null ) {
                return Problem( "Entity set 'SpartaToDoContext.ToDos'  is null." );
            }
            var toDo = await _context.ToDos.FindAsync( id );
            if ( toDo != null ) {
                _context.ToDos.Remove( toDo );
            }

            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
        }

        private bool ToDoExists( int id ) {
            return ( _context.ToDos?.Any( e => e.Id == id ) ).GetValueOrDefault();
        }
    }
}
