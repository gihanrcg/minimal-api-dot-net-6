using FirstMinimalApi.RouteHelper;

namespace FirstMinimalApi.routes
{
    public static class RouteDefinitionExtensions
    {
        public static void AddRouterDefinitions(this IServiceCollection services, params Type[] scanMarkers)
        {
            var endpointDefinitions = new List<IRouterDefinition>();

            foreach(var marker in scanMarkers)
            {
                endpointDefinitions.AddRange(
                    marker.Assembly.ExportedTypes
                    .Where(x => typeof(IRouterDefinition).IsAssignableFrom(x) && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IRouterDefinition>()
                    );
            }

            foreach(var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.DefineServices(services);
            }

            services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IRouterDefinition>);
        }

        public static void UseRouterDefinitions(this WebApplication app)
        {
            var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IRouterDefinition>>();
            foreach (var routerDefinition in definitions)
            {
                routerDefinition.DefineRoutes(app);
            }
        }
    }


}
