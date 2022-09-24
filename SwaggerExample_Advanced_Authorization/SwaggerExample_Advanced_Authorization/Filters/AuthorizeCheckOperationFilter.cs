using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerExample_Advanced_Authorization.Filters {
  public class AuthorizeCheckOperationFilter : IOperationFilter {
    private readonly EndpointDataSource _endpointDataSource;

    public AuthorizeCheckOperationFilter(EndpointDataSource endpointDataSource) {
      _endpointDataSource = endpointDataSource;
    }
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
      var Descriptor = _endpointDataSource.Endpoints.FirstOrDefault(x =>
          x.Metadata.GetMetadata<ControllerActionDescriptor>() == context.ApiDescription.ActionDescriptor);

      var Authorize = Descriptor.Metadata.GetMetadata<AuthorizeAttribute>() != null;

      var AllowAnonymous = Descriptor.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;

      if (!Authorize || AllowAnonymous)
        return;

      operation.Security = new List<OpenApiSecurityRequirement>
      {
                new()
                {
                    [
                        new OpenApiSecurityScheme {Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"}
                        }
                    ] = new List<string>()
                }
            };
    }
  }
}
