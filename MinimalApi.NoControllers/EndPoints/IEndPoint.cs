namespace MinimalApi.NoControllers.EndPoints;
/// <summary>
/// EndPoint Flag inteface
/// </summary>
public interface IEndPointDefinition
{
    void MapRoutes(WebApplication app);
}