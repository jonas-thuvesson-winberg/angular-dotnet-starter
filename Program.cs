using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
// using Microsoft.AspNetCore.SpaServices.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors();

        builder.Services.AddSpaStaticFiles(
            (x) => x.RootPath = "client-app/dist/"
        );

        var app = builder.Build();

        app.UseCors((x) =>
        {
            x.AllowAnyHeader();
            x.AllowAnyMethod();
            x.WithOrigins("http://localhost:4200");
        });

        app.MapWhen(r => !r.Request.Path.StartsWithSegments("/api"), client =>
        {
            client.UseSpa(
                (spa) =>
                {
                    spa.Options.SourcePath = "client-app";
                    spa.Options.DefaultPage = $"/index.html";

                    Console.WriteLine("Environment is... : " + app.Environment.EnvironmentName);
                    if (app.Environment.IsDevelopment())
                    {
                        spa.Options.DevServerPort = 4200;
                        spa.UseAngularCliServer(npmScript: "start");

                    }
                    else
                    {
                        client.UseSpaStaticFiles();
                    }
                });
        });

        app.MapGet("/api/hello", () => new { res = "Hello World!" });

        app.Run();
    }
}