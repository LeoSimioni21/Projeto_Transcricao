using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.Linq;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParameter = context.ApiDescription.ActionDescriptor.Parameters
            .FirstOrDefault(p => p.ParameterType == typeof(IFormFile));

        if (fileParameter != null)
        {
            var fileContent = operation.Parameters
                .FirstOrDefault(p => p.Name == "audioFile");

            if (fileContent != null)
            {
                fileContent.Schema.Type = "string";
                fileContent.Schema.Format = "binary";
            }
        }
    }
}
