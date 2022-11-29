using Microsoft.EntityFrameworkCore;

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

app.MapPost("/todoitems", async (Todo todo, TodoDB db) => {
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todo.ID}", todo);    
});

app.MapPut("/todoitems/{id}", async (Int64 id, Todo input, TodoDB db) => {
    var todo = await db.Todos.FindAsync(id);
    
    if (todo is null) {
        return Results.NotFound();
    }

    todo.Title = input.Title;
    todo.IsComplete = input.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (Int64 id, TodoDB db) => {
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) {
        return Results.NotFound();
    }

    db.Todos.Remove(todo);

    await db.SaveChangesAsync();
    return Results.Ok(todo);
});

app.Run();


static async Task<IResult> GetAllTodos (TodoDB db) {
    var todos  = await db.Todos.ToListAsync();
    return Results.Ok(todos);
}

static async Task<IResult> GetCompleteTodos (TodoDB db) {
    var todoCompleted = await db.Todos
        .Where( item => item.IsComplete )
        .ToListAsync();

    return Results.Ok(todoCompleted);
}

static async Task<IResult> GetTodoById(Int64 id, TodoDB db) {
    Todo? todo  = await db.Todos.FindAsync(id);
    if (todo is null) {
        return Results.NotFound();
    }
    return Results.Ok(todo);
}