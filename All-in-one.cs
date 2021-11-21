using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<UserRepository>();

var app = builder.Build();

app.MapGet("/users", ([FromServices] UserRepository repo) =>
{
    return Results.Ok(repo.GetAll());
});

app.MapGet("/users/{id}", ([FromServices] UserRepository repo, Guid id) =>
{
   var user = repo.GetById(id) ;
    return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.MapPost("/users", ([FromServices] UserRepository repo, User user) =>
{
    repo.Create(user);
    return Results.Created($"/users/{user.Id}", user);
});

app.MapDelete("/users/{id}", ([FromServices] UserRepository repo, Guid id) =>
{
    repo.Delete(id);
    return Results.Ok($"Removed /users/{id}");
});


app.Run("http://localhost:3000");

record User(Guid Id, String name);

class UserRepository
{
    private readonly Dictionary<Guid, User> _users = new();

    public void Create(User user)
    {
        if(user == null)
        {
            return;
        }
        _users[user.Id] = user;
    }

    public User GetById(Guid Id)
    {
        return _users[Id];
    }

    public List<User> GetAll()
    {
        return _users.Values.ToList();
    }

    public void Update(User user)
    {
        User existingUser = GetById(user.Id);
        if (existingUser is null)
        {
            return;
        }  
            _users[existingUser.Id] = user; ;
        
    }

    public void Delete(Guid Id)
    {
        _users.Remove(Id);
    }
    
}