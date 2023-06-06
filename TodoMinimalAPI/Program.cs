using Microsoft.EntityFrameworkCore;
using TodoMinimalAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


var todoItems = app.MapGroup("/todoitems");

todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/complete", GetCompeleTodos);
todoItems.MapGet("/{id}", GetTodo);
todoItems.MapPost("/", CreateTodo);
todoItems.MapPut("/{id}", UpdateTodo);
todoItems.MapDelete("/{id}", DeleteTodo);

app.Run();

static async Task<IResult> GetAllTodos(TodoDb db)
{
    return TypedResults.Ok(await db.Todos.Select(x=> new TodoDTO(x)).ToArrayAsync());
}

static async Task<IResult> GetCompeleTodos(TodoDb db)
{
    return TypedResults.Ok(await db.Todos.Where(x=> x.IsComplete).Select(x => new TodoDTO(x)).ToArrayAsync());
}

static async Task<IResult> GetTodo(int id, TodoDb db)
{
    return await db.Todos.FindAsync(id) is Todo todo ? TypedResults.Ok(new TodoDTO(todo)) : TypedResults.NotFound();
}

static async Task<IResult> CreateTodo(TodoDTO todoDto, TodoDb db)
{
    var todo = new Todo()
    {
        Name = todoDto.Name,
        IsComplete = todoDto.IsComplete,
    };

    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    todoDto = new TodoDTO(todo);

    return TypedResults.Created($"/todoitems/{todoDto.Id}", todoDto);
}

static async Task<IResult> UpdateTodo(int id, TodoDTO todoDto,  TodoDb db)
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return TypedResults.NotFound();

    todo.Name = todoDto.Name;
    todo.IsComplete = todoDto.IsComplete;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}
static async Task<IResult> DeleteTodo(int id,  TodoDb db)
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.Ok(new TodoDTO(todo));
    }
    return TypedResults.NotFound();
}


