using FirstMinimalApi.models;
using FirstMinimalApi.repositories;
using FirstMinimalApi.RouteHelper;

namespace FirstMinimalApi.routes
{
    public class UserRoute : IRouterDefinition
    {
        public void DefineRoutes(WebApplication app) 
        {

            app.MapGet("/users", (UserRepository repo) =>
            {
                return Results.Ok(repo.GetAll());
            }).Produces<List<User>>();

            app.MapGet("/users/{id}", ( UserRepository repo, Guid id) =>
            {
                var user = repo.GetById(id);
                return user is not null ? Results.Ok(user) : Results.NotFound();
            });

            app.MapPost("/users", ( UserRepository repo, User user) =>
            {
                repo.Create(user);
                return Results.Created($"/users/{user.Id}", user);
            });

            app.MapDelete("/users/{id}", ( UserRepository repo, Guid id) =>
            {
                repo.Delete(id);
                return Results.Ok($"Removed /users/{id}");
            });
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<UserRepository>();
        }
    }
}
