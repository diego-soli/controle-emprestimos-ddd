using EmprestimoLivros.API.Errors;
using System.Net;
using System.Text.Json;

namespace EmprestimoLivros.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; // É uma das partes do ciclo de vida de uma requisição.
        private readonly ILogger<ExceptionMiddleware> _logger; // serve para formatar o erro
        private readonly IHostEnvironment _env; // identifico se a app esta em producao ou desenvolvimento

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // aguardar a requisição
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message); //formatação do erro
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ? 
                    new ApiException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace.ToString()) :
                    new ApiException(context.Response.StatusCode.ToString(), ex.Message, "Internal server error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
