namespace WebApi.Endpoints;

public class HelloWorldEndpoints : EndpointsGroupBase
{
    public override void Map(WebApplication app)
    {
        var helloWorld = app.MapGroup(this);

        helloWorld.MapGet("", () => "Hello World");
    }
}
