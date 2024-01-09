using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Renave.Anfir.Controllers
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings SerializerSettings { get; set; }

        public JsonNetResult()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                var jsonTextWriter = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };

                var serializer = JsonSerializer.Create(SerializerSettings);
                serializer.Serialize(jsonTextWriter, Data);

                jsonTextWriter.Flush();
            }
        }
    }

    public class QAController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExecutarConsulta(string query, string connectionString, string username, string password)
        {
            if (username == "dev" && password == "Dev5231@")
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        if (query.ToLower().StartsWith("select"))
                        {
                            var result = connection.Query(query);
                            return new JsonNetResult { Data = result };
                        }
                        else if (query.ToLower().StartsWith("update") || query.ToLower().StartsWith("delete"))
                        {
                            int affectedRows = connection.Execute(query);
                            return new JsonNetResult { Data = new { success = true, successMessage = "Query executada com sucesso", affectedRows } };
                        }
                        else
                        {
                            return new JsonNetResult { Data = new { error = "Tipo de consulta não suportado." } };
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new JsonNetResult { Data = new { error = ex.Message } };
                }
            }
            else
            {
                return new JsonNetResult { Data = new { error = "Login ou senha inválidos." } };
            }
        }
    }
}
