using APIEvents.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;

namespace APIEvents.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<CityEventRepository> _logger;

        public GeneralExceptionFilter(ILogger<CityEventRepository> logger)
        {
            _logger = logger;      
        }
        public override void  OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails()
            {
                Status = 500,
                Title = "Erro inesperado, tente Novamente",
                Detail = "Ocorreu um erro inesperado na solitação",
                Type = context.Exception.GetType().Name
            };

            switch (context.Exception)
            {
                case SqlException ex:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    problem.Title = "Erro inesperado ao se comunicar com o banco de dados";
                    problem.Detail = "Falha não reconhecida ao se conectar com o Banco de Dados";
                    problem.Type = context.Exception.GetType().Name;
                    problem.Status = 503;
                    context.Result = new ObjectResult(problem);
                    _logger.LogError(ex, "Houve um erro inesperado na requisão da API na conexão com o Banco de Dados");
                    break;
                case NullReferenceException ex:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status417ExpectationFailed;
                    problem.Title = "Erro inesperado no sistema";
                    problem.Detail = "Falha não reconhecida no sistema";
                    problem.Type = context.Exception.GetType().Name;
                    problem.Status = 417;
                    context.Result = new ObjectResult(problem);
                    _logger.LogError(ex, "Houve um erro inesperado na requisão da API de um NullReferenceException");
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    _logger.LogError("Houve um erro inesperado na requisão da API");
                    break;
            } 
        }
    }
}
