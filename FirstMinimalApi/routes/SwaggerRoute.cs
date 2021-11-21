using FirstMinimalApi.RouteHelper;
using Microsoft.AspNetCore.Builder;

namespace FirstMinimalApi.routes
{
    public class SwaggerRoute : IRouterDefinition
    {
        public void DefineRoutes(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FirstMinimalApi"));
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "FirstMinimalApi", Version = "v1" });
            });
        }
    }
}
