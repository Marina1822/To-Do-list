
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TodoDatabase")
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(
            "http://127.0.0.1:5500",
            "http://localhost:5500"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

/*var todoItems = new List<TodoItem>
{
    new TodoItem { Id = 1, Title = "Write project plan", IsCompleted = false },
    new TodoItem { Id = 2, Title = "Build to-do API", IsCompleted = true }
};*/

app.MapGet("/todos", async (TodoDbContext db) =>
{
    var todos = await db.Todos.ToListAsync();
    return Results.Ok(todos);
});

app.MapGet("/todos/{id:int}", async (int id, TodoDbContext db) =>
{
    var item = await db.Todos.FindAsync(id);

    return item is null
        ? Results.NotFound()
        : Results.Ok(item);
});

app.MapPost("/todos", async (CreateTodoRequest request, TodoDbContext db) =>
{
    var todo = new TodoItem
{
    Title = request.Title,
    IsCompleted = request.IsCompleted
};

    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id:int}", async (int id, UpdateTodoRequest request, TodoDbContext db) =>
{
    var item = await db.Todos.FindAsync(id);

    if (item is null)
    {
        return Results.NotFound();
    }

    item.Title = request.Title;
    item.IsCompleted = request.IsCompleted;

    await db.SaveChangesAsync();

    return Results.Ok(item);
});

app.MapDelete("/todos/{id:int}", async (int id, TodoDbContext db) =>
{
    var item = await db.Todos.FindAsync(id);

    if (item is null)
    {
        return Results.NotFound();
    }

    db.Todos.Remove(item);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}

public record CreateTodoRequest(string Title, bool IsCompleted = false);
public record UpdateTodoRequest(string Title, bool IsCompleted = false);
