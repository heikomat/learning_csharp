using System;

using Microsoft.AspNetCore.Hosting; // contains the WebHostBuilder and knows which package contains the applicationBuilder-Interface
using Microsoft.AspNetCore.Builder; // contains the applicationBuilder-Interface
using Microsoft.AspNetCore.Http;    // contains the httpContext.Response-Interface

namespace mini_http
{
    class Program
    {
        static void Main(string[] args)
        {
            new WebHostBuilder() // Initialize a new WebHostBuilder
                .UseKestrel()    // Tell the WebHostBuilder to use Kestrel als the server
                .Configure((applicationBuilder) => { // define a callback in which the webApplication can be configured

                    // Add a middleware that can run logic for all requests to all routes
                    applicationBuilder.Use((context, next) =>
                    {
                        Console.WriteLine("Middleware for the whole application!");
                        return next(); // call the next middleware
                    });

                    // configure a route (localhost:5000/route1) and its subroutes (localhost:5000/route1/subroute1)
                    applicationBuilder.Map("/route1", HandleRoute1);

                    // handle all other routes
                    // this has to come last, so that the middlewares and mapped routes are found first in the request-route-mapping
                    applicationBuilder.Run((httpContext) =>
                    {
                        return httpContext.Response.WriteAsync("i don't know that route");
                    });
                })
                .Build()
                .Run();
        }

        public static void HandleRoute1(IApplicationBuilder route1ApplicationBuilder)
        {
            // configure another route (localhost:5000/route1/subroute1)
            route1ApplicationBuilder.Map("/subroute1", HandleSubroute1);

            route1ApplicationBuilder.Run((httpContext) =>
            {
                return httpContext.Response.WriteAsync("this is route 1!");
            });
        }

        public static void HandleSubroute1(IApplicationBuilder subroute1ApplicationBuilder)
        {
            // Add a middleware that can run logic for all requests to all subroutes of /route1/subroute1
            subroute1ApplicationBuilder.Use((context, next) =>
            {
                Console.WriteLine("Middleware for some small part of the application!");
                return next(); // call the next middleware
            });

            // handle all requests to localhost:5000/route1/subroute1
            subroute1ApplicationBuilder.Run((httpContext) =>
            {
                return httpContext.Response.WriteAsync("this is a subroute of route 1!");
            });
        }
    }
}
