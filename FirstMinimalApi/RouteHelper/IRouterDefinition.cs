namespace FirstMinimalApi.RouteHelper
{
    public interface IRouterDefinition
    {
        void DefineRoutes(WebApplication app);

        void DefineServices(IServiceCollection services);
    }
}
