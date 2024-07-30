using App.APIs.Swagger;
using App.Core.ServicesDI;
using App.Infrastructure.AppDI;
using App.Infrastructure.DBContext;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


ServicesDI.AddApplicationDI(builder.Services);
AppDI.AddInfrastructureDI(builder.Services,builder.Configuration);



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "EgyptianRecipesApp", Version = "v1" });
    options.OperationFilter<SwaggerFilter>();
    options.CustomSchemaIds(type => type.ToString().Replace("+", "."));// A solution for The same schemaId is already used for type(bla bla bla )exception
                                                                       //options.DescribeAllEnumsAsStrings();
                                                                       //var filePath = Path.Combine(AppContext.BaseDirectory, "App.Api.xml");
                                                                       //options.IncludeXmlComments(filePath);

    // Swagger 2.+ support
    var security = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                    //{"Bearer", new string[] { }},
                };


    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(security);
});

builder.Services.AddMvc().AddNewtonsoftJson(option =>
{
    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

});
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<dbContext>();
    await db.Database.MigrateAsync();
    await db.DisposeAsync();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<dbContext>();
   // await db.Database.MigrateAsync();
    await db.DisposeAsync();
}

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EgyptianRecipes-WebApi V1/");
    c.DocumentTitle = "EgyptianRecipesApp Documentation";
    c.DocExpansion(DocExpansion.None);
});
app.UseCors(c => c.AllowAnyMethod()
                 .WithOrigins()
                 .AllowAnyHeader()
                 .SetIsOriginAllowed(orgin => true)
                 .AllowCredentials());
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();
