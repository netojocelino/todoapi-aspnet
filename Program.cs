using Microsoft.EntityFrameworkCore;

using DTO;
using Model;
using Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDB>( opt => opt.UseInMemoryDatabase("TodoList"));
/* TODO: why doen't work? */
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

/* TODO: why doen't work? */
// var todoItems = app.MapGroup("/todoitems");

app.MapGet("/todoitems", GetAllTodos );
app.MapGet("/todoitems/complete", GetCompleteTodos );
app.MapGet("/todoitems/{id}", GetTodoById);

app.MapPost("/todoitems", PostTodo);

app.MapPut("/todoitems/{id}", PutTodo);

app.MapDelete("/todoitems/{id}", DeleteTodo);

app.Run();


static async Task<IResult> GetAllTodos (TodoDB db) {
    var todos  = await db.Todos
            .Select( item => new TodoItemDTO(item))
            .ToListAsync();
    /* TODO: why TypedResults doesn't work? */
    return Results.Ok(todos);
}

static async Task<IResult> GetCompleteTodos (TodoDB db) {
    var todoCompleted = await db.Todos
        .Where( item => item.IsComplete )
        .Select( item => new TodoItemDTO(item))
        .ToListAsync();

    return Results.Ok(todoCompleted);
}

static async Task<IResult> GetTodoById(Int64 id, TodoDB db) {
    Todo? todo  = await db.Todos.FindAsync(id);
    if (todo is null) {
        return Results.NotFound();
    }
    return Results.Ok(new TodoItemDTO(todo));
}

static async Task<IResult> PostTodo(TodoItemDTO todo, TodoDB db) {
    var item = new Todo
    {
        IsComplete = todo.IsComplete,
        Title = todo.Title
    };

    db.Todos.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{item.ID}", item);
}


static async Task<IResult> PutTodo(Int64 id, TodoItemDTO input, TodoDB db) {
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) {
        return Results.NotFound();
    }

    todo.Title = input.Title;
    todo.IsComplete = input.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
}

static async Task<IResult> DeleteTodo(Int64 id, TodoDB db) {
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) {
        return Results.NotFound();
    }

    db.Todos.Remove(todo);

    await db.SaveChangesAsync();
    return Results.Ok(todo);
}