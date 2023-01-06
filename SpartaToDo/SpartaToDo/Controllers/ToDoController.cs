using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpartaToDo.Models;
using SpartaToDo.Models.ViewModels;
using SpartaToDo.Services;

namespace SpartaToDo.Controllers {
    public class ToDoController : Controller {
        private readonly IToDoService _service;

        public ToDoController( IToDoService service ) {
            _service = service;
        }

        // GET: ToDo
        public async Task<IActionResult> Index( string filter = "" ) {

            if ( _service.NoToDos() ) {
                return Problem( "There are no ToDos to do!" );
            }
            if ( string.IsNullOrEmpty( filter ) ) {
                return View( await _service.GetAllAsync() );
            }

            return View(
                ( await _service.GetAllAsync() )
                    .Where( td =>
                        td.Title.Contains(
                            filter, StringComparison.OrdinalIgnoreCase ) ||
                        td.Description!.Contains(
                            filter, StringComparison.OrdinalIgnoreCase )
                        ) );
        }

        // GET: ToDo/Details/5
        public async Task<IActionResult> Details( int? id ) {

            if ( id == null || _service.NoToDos() ) {
                return NotFound();
            }

            var toDo = await _service.FindOneAsync( id );
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
                await _service.AddAsync( toDo );
                return RedirectToAction( nameof( Index ) );
            }
            model.PageTitle = "Create";
            return View( model );
        }

        // GET: ToDo/Edit/5
        public async Task<IActionResult> Edit( int? id ) {

            if ( id == null || _service.NoToDos() ) {
                return NotFound();
            }

            var toDo = await _service.FindOneAsync( id );
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
                    await _service.UpdateAsync( toDo );
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

            if ( id == null || _service.NoToDos() ) {
                return NotFound();
            }

            var toDo = await _service.FindOneAsync( id );
            if ( toDo == null ) {
                return NotFound();
            }

            return View( toDo );
        }

        // POST: ToDo/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed( int id ) {

            if ( _service.NoToDos() ) {
                return Problem( "Entity set 'SpartaToDoContext.ToDos'  is null." );
            }
            var toDo = await _service.FindOneAsync( id );
            if ( toDo != null ) {
                await _service.DeleteAsync( toDo );
            }

            await _service.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
        }
        private bool ToDoExists( int id ) {
            return ( _service.Exists( id ) );
        }

    }
}
