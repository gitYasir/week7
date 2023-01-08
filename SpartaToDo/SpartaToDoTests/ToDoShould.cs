using Microsoft.AspNetCore.Mvc;
using Moq;
using SpartaToDo.Controllers;
using SpartaToDo.Models;
using SpartaToDo.Services;

namespace SpartaToDoTests {
    public class ToDoControllerTests {
        private ToDoController? _sut;

        #region Index Tests

        [Test]
        [Category( "Index Tests" )]
        public async Task Index_ReturnsViewResult_WhenToDosExist() {
            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.GetAllAsync() ).ReturnsAsync( new List<ToDo>() );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Index();

            Assert.IsInstanceOf<ViewResult>( result );
        }

        [Test]
        [Category( "Index Tests" )]
        public async Task Index_ReturnsViewResult_WithExactNumberOfToDos() {
            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.GetAllAsync() ).ReturnsAsync( new List<ToDo> { new ToDo { Title = "Task 1" }, new ToDo { Title = "Task 2" } } );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Index();

            var viewResult = result as ViewResult;
            var model = viewResult.Model as List<ToDo>;
            Assert.That( model, Has.Count.EqualTo( 2 ) );
        }

        [Test]
        [Category( "Index Tests" )]
        public async Task Index_ReturnsViewResult_ListOfToDos() {
            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.GetAllAsync() ).ReturnsAsync( new List<ToDo>() );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Index();

            var viewResult = result as ViewResult;
            Assert.That( viewResult.Model, Is.InstanceOf<List<ToDo>>() );
        }

        #endregion

        #region Details Tests

        [Test]
        [Category( "Details Tests" )]
        public async Task Details_ReturnsViewResult() {
            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.FindOneAsync( It.IsAny<int>() ) ).ReturnsAsync( new ToDo() );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Details( It.IsAny<int>() );
            Assert.IsInstanceOf<ViewResult>( result );
        }

        [Test]
        [Category( "Details Tests" )]
        public async Task Details_ReturnsNotFound_WhenNoToDos() {
            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.NoToDos() ).Returns( true );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Details( It.IsAny<int>() );
            Assert.IsInstanceOf<NotFoundResult>( result );
        }

        [Test]
        [Category( "Details Tests" )]
        public async Task Details_ReturnsNotFound_WhenNoIdGiven() {
            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.NoToDos() ).Returns( true );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Details( null );
            Assert.IsInstanceOf<NotFoundResult>( result );
        }

        #endregion

        #region Create Tests

        [Test]
        [Category( "Create Tests" )]
        public async Task Create_ReturnsViewModel() {
            var ms = new Mock<IToDoService>();
            _sut = new ToDoController( ms.Object );
            var result = _sut.Create();
            Assert.IsInstanceOf<ViewResult>( result );
        }

        #endregion

        #region Edit Tests

        [Test]
        [Category( "Edit Tests" )]
        public async Task Edit_ReturnsViewResult_WhenValidIdGiven() {

            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.FindOneAsync( It.IsAny<int>() ) ).ReturnsAsync( new ToDo() );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Edit( It.IsAny<int>() );
            Assert.IsInstanceOf<ViewResult>( result );
        }

        [Test]
        [Category( "Edit Tests" )]
        public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull() {

            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.FindOneAsync( null ) ).ReturnsAsync( ( ToDo ) null );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Edit( It.IsAny<int>() );
            Assert.IsInstanceOf<NotFoundResult>( result );
        }

        [Test]
        [Category( "Edit Tests" )]
        public async Task Edit_ReturnsNotFoundResult_WhenToDoIsNull() {

            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.FindOneAsync( It.IsAny<int>() ) ).ReturnsAsync( ( ToDo ) null );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Edit( It.IsAny<int>() );
            Assert.IsInstanceOf<NotFoundResult>( result );
        }

        #endregion

        #region Delete Tests

        [Test]
        [Category( "Delete Tests" )]
        public async Task Delete_ReturnsViewResult_WhenValidIdGiven() {
            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.FindOneAsync( It.IsAny<int>() ) ).ReturnsAsync( new ToDo() );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Delete( It.IsAny<int>() );
            Assert.IsInstanceOf<ViewResult>( result );
        }

        [Test]
        [Category( "Delete Tests" )]
        public async Task Delete_ReturnsNotFoundResult_WhenNullIdGiven() {
            var ms = new Mock<IToDoService>();
            _sut = new ToDoController( ms.Object );
            var result = await _sut.Delete( null );
            Assert.IsInstanceOf<NotFoundResult>( result );
        }

        [Test]
        [Category( "Delete Tests" )]
        public async Task Delete_ReturnsRedirectToActionResult_WhenValidIdGiven() {
            var ms = new Mock<IToDoService>();
            ms.Setup( s => s.FindOneAsync( It.IsAny<int>() ) ).ReturnsAsync( new ToDo() );
            _sut = new ToDoController( ms.Object );
            var result = await _sut.DeleteConfirmed( It.IsAny<int>() );
            Assert.IsInstanceOf<RedirectToActionResult>( result );

        }

        #endregion

    }
}
