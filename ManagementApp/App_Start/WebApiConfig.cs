using ManagementApp.App_Start;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ManagementApp
{
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            AutofacConfig.Initialize(config);

            // Web API routes
            config.MapHttpAttributeRoutes();


            var cors = new EnableCorsAttribute("*", "*", "GET");

            config.EnableCors();





            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
