using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoApi.Controllers;
using TodoApi.Models;

namespace Todo.Test
{
    [TestClass]
    public class TodoUnitTest
    {
        private TodoContext _context;
        private TodoItemsController _todoItemsController;
        private DbContextOptions<TodoContext> ContextOptions { get; }

        //public TodoUnitTest(TodoContext context)
        //{
        //    _context = context;
        //    _todoItemsController = new TodoItemsController(context);
        //}

        [TestInitialize]
        public void Initalize()
        {
            //_context = new TodoContext(ContextOptions);

            var options = new DbContextOptionsBuilder<TodoContext>()
                            .UseInMemoryDatabase(databaseName: "testTodoDB")
                            .Options;
            var dbContext = new TodoContext(options);
            //var mockSet = new Mock<TodoContext>();
            _todoItemsController = new TodoItemsController(dbContext);
        }

        [TestMethod]
        public void CreateTodoItem()
        {
            var toDoItem = new TodoItem()
            {
                Id = 1,
                Name = "TestTodoItem",
                IsComplete = false
            };

            var toDoItemCreatedReponse = _todoItemsController.PostTodoItem(toDoItem).Result;
            var response = Xunit.Assert.IsType<ActionResult<TodoItem>>(toDoItemCreatedReponse);
            //var code =response.Result.StatusCode;
            //Assert.IsInstanceOfType(toDoItemCreatedReponse, typeof(CreatedAtActionResult));
            Assert.AreEqual(201, ((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).StatusCode.Value);
        }

        [TestMethod]
        [DataRow(1)]
        public void TodoItemById(long id)
        {
            var todoItem = _todoItemsController.GetTodoItem(id);
            Assert.AreEqual(todoItem.Id, id);
        }


        [TestMethod]
        public void UpdateTodoItem()
        {
            var toDoItem = new TodoItem()
            {
                Id = 1,
                Name = "TestTodoItem",
                IsComplete = true
            };

            var toDoItemCreatedReponse = _todoItemsController.PutTodoItem(toDoItem.Id,toDoItem).Result;
            var updatedTodoItem = _todoItemsController.GetTodoItem(toDoItem.Id).Result;
            Assert.AreEqual(updatedTodoItem.Value.IsComplete, toDoItem.IsComplete);
        }
    }
}