﻿// ComposyGeneratorApi.ComposyGeneratorApi.Startup.cs
// 
// Generated By 
// Created At 29-06-2017

using System.Web.Http;
using System.Web.Http.Routing;
using Beginor.Owin.StaticFile;
using Owin;

namespace ComposyGeneratorApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            
            config.EnableCors();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            appBuilder.UseStaticFile(new StaticFileMiddlewareOptions
            {
                RootDirectory = @"C:\inetpub\wwwroot",
                DefaultFile = "iisstart.htm",
                EnableETag = true,
                MimeTypeProvider = new MimeTypeProvider()
            });

            appBuilder.UseWebApi(config);
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}