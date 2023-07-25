using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyProtocolsAPI_WirvingJ.Attributes
{




    [AttributeUsage(validOn: AttributeTargets.All)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        //especificamos cual es el valor apikey
        private readonly string _apiKey = "Progra6Apikey";
         
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        { 
            //acá validamos que en el body (en tipo json) del request vaya la info de la ApiKey
            //si no va la info presentamos un mensaje de error indicándo que falta ApiKey y que no se 
            //puede consumir el recurso. 

            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Llamada no contiene información de seguridad..."
                };
                return;
                //si no hay info de segurida sale de la funcion y muestra este mensaje
            
            }

            //si viene info de seguridad falta validar que sea la correcta
            
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = appSettings.GetValue<string>(_apiKey);

            //queda comparar que las apikey sean iguales
            if (!ApiKeyValue.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey Inválida..."
                };

                return;
            }

            await next();

        }

    }
}