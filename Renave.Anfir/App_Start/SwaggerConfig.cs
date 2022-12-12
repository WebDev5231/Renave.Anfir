using System.Web.Http;
using WebActivatorEx;
using Renave.Anfir;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Renave.Anfir
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Renave.Anfir");
                    })
                .EnableSwaggerUi(c =>
                    {
                    });
        }
    }
}
