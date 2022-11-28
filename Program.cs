using Microsoft.EntityFrameworkCore;

using Model;
using Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDB>( opt => opt.UseInMemoryDatabase("TodoList"));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/todoitems", async (TodoDB db) => await db.Todos.ToListAsync() );
app.MapGet("/todoitems/complete", async (TodoDB db) => await db.Todos.Where( item => item.IsComplete).ToListAsync() );
app.MapGet("/todoitems/{id}", async (Int64 id, TodoDB db) => await db.Todos.FindAsync(id) is Todo todo ? Results.Ok(todo) : Results.NotFound() );

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
