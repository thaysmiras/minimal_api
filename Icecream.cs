using NSwag.AspNetCore;
using Icecream.Models;

class HelloWeb
{
    static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "TodoAPI";
            config.Title = "TodoAPI v1";
            config.Version = "v1";
            //qaaaa
        });

        WebApplication app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi(config =>
            {
                config.DocumentTitle = "Icecream API";
                config.Path = "/swagger";
                config.DocumentPath = "/swagger/{documentName}/swagger.json";
                config.DocExpansion = "list";
            });
        }

        //app.MapGet("/", () => "Hello World!");
        // app.MapGet("/", () => new Flavor(Guid.NewGuid(), "Morango"));


        /*      List<Flavor> flavors = new List<Flavor>();
                flavors.Add(new Flavor(Guid.NewGuid(), "Morango"));
                flavors.Add(new Flavor(Guid.NewGuid(), "Chocolate"));
                flavors.Add(new Flavor(Guid.NewGuid(), "Pêra"));
         */

        List<Flavor> flavors = new()
        {
            new Flavor(Guid.NewGuid(), "Morango"),
            new Flavor(Guid.NewGuid(), "Chocolate"),
            new Flavor(Guid.NewGuid(), "Pêra")
        };


        app.MapGet("/api/flavors", () =>
        {
            return Results.Ok(flavors);
        });

        app.MapPut("/api/flavors", (Flavor inputFlavor) =>
        {
            if (inputFlavor is null) return Results.NotFound();

            foreach (var flavor in flavors)
            {
                if (flavor.Id == inputFlavor.Id)
                {
                    flavor.Name = inputFlavor.Name;
                    return Results.Ok(flavor);
                }
            }

            return Results.NotFound();
        });

        app.MapPost("/api/flavors", (string name) =>
        {
            var newFlavor = new Flavor(Guid.NewGuid(), name);
            flavors.Add(newFlavor);

            Results.Ok(newFlavor);
        });

        app.MapDelete("/api/flavors", (Guid id) =>
        {
            foreach (var flavor in flavors)
            {
                if (flavor.Id.CompareTo(id) == 0)
                {
                    flavors.Remove(flavor);
                    return Results.Ok(flavor);
                }
            }

            return Results.NotFound();
        });

        app.Run();
    }
}
