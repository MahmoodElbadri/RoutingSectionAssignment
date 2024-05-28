using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using RoutingSectionAssignment.CustomNumberConstraint;
using System.Collections.Generic;
using System.Text.Json;

namespace RoutingSectionAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRouting(options =>
            {
                options.ConstraintMap.Add("mini", typeof(NumberConstraint));
            });
            var app = builder.Build();

            Dictionary<int, string> countries = new Dictionary<int, string>
            {
                { 1, "United States" },
                { 2, "Canada" },
                { 3, "United Kingdom" },
                { 4, "India" },
                { 5, "Japan" }
            };

            app.UseRouting();

            app.UseEndpoints(ep =>
            {
                ep.MapGet("/countries", async ctx =>
                {
                    ctx.Response.StatusCode = 200;
                    ctx.Response.ContentType = "application/json";

                    var json = JsonSerializer.Serialize(countries);
                    await ctx.Response.WriteAsync(json);
                });

                ep.MapGet("/countries/{countryID:int:mini}", async ctx =>
                {
                    int countryId = Convert.ToInt32(ctx.Request.RouteValues["countryID"]);
                    if (countries.ContainsKey(countryId))
                    {
                        ctx.Response.StatusCode = 200;
                        var country = JsonSerializer.Serialize(countries[countryId]);
                        await ctx.Response.WriteAsync(country);
                    }
                    else if (countryId <= 100)
                    {
                        ctx.Response.StatusCode = 404;
                        await ctx.Response.WriteAsync("[No Country]");
                    }
                });
                ep.MapGet("/countries/{countryID}", async ctx =>
                {
                    ctx.Response.StatusCode = 400;
                    await ctx.Response.WriteAsync("The CountryID should be between 1 and 100!");
                });
            });

            app.Run();
        }
    }
}
