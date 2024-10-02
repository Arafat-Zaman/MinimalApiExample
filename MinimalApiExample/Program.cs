using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// GET endpoint: Returns a welcome message
app.MapGet("/", () => "Welcome to Minimal API in .NET 8!");

// GET endpoint: Returns the square of a number
app.MapGet("/square/{number:int}", (int number) => Results.Ok(new
{
    Input = number,
    Square = number * number
}));

// POST endpoint: Accepts a user object and returns it with a success message
app.MapPost("/create-user", async (HttpContext context) =>
{
    var user = await context.Request.ReadFromJsonAsync<User>();
    if (user != null)
    {
        return Results.Ok(new { Message = "User created successfully", user });
    }
    return Results.BadRequest("Invalid user data");
});

app.Run();

record User(string FirstName, string LastName, int Age);
