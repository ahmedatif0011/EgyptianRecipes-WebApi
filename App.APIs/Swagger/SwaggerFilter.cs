using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.APIs.Swagger
{
    public class SwaggerFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "CurrentBranchId",
            //    In = ParameterLocation.Header,
            //    Schema = new OpenApiSchema { Type = "int" },
            //    //Type = "string",
            //    Required = false // set to false if this is optional
            //});
        }

    }
}
