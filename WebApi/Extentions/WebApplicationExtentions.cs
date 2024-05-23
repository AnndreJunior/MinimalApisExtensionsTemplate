using System.Reflection;

namespace WebApi.Extentions;

public static class WebApplicationExtentions
{
    /// <summary>
    /// Extends the MapGroup method
    /// </summary>
    /// <remarks>
    /// That's optional, but allows you to do something like this:
    ///     app.MapGroup(this);
    /// If you prefer, you don't need to create this method and just do this:
    ///     app.MapGroup("/your-endpoints-group");
    /// </remarks>
    /// <param name="app">Instance of WebApplication</param>
    /// <param name="group">Base class to create your endpoints</param>
    /// <returns>An instance of WebApplication with map group, tags and open api</returns>
    public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointsGroupBase group)
    {
        var groupName = group.GetType().Name.Split("Endpoints");

        return app
            .MapGroup(groupName[0])
            .WithTags(groupName[0])
            .WithOpenApi();
    }

    public static void MapEndpoints(this WebApplication app)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var endpointsGroupBaseTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(typeof(EndpointsGroupBase)));

        foreach (var type in endpointsGroupBaseTypes)
        {
            if (Activator.CreateInstance(type) is EndpointsGroupBase instance)
                instance.Map(app);
        }
    }
}
