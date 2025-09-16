using Microsoft.OpenApi.Models;
using SB.PruebaTecnica.Api.Extensions;
using PruebaTecnica.Documentation;
using SB.PruebaTecnica.Infrastructure;
using SB.PruebaTecnica.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration)=> configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddControllers();

// builder.Services.ConfigureOptions<JwtOptionsSetup>();
// builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

// builder.Services.AddTransient<IJwtProvider, JwtProvider>();

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
//Esta configuracion es si el modelo como User es primitivo
// builder.Services.AddSwaggerGen();

//Esta configuracion es para como lo tenemos ahora con object values
builder.Services.AddSwaggerGen(options => {
    options.CustomSchemaIds( type => type.ToString());

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Add your specific origins here
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        var descriptions = app.DescribeApiVersions();

        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name); 
        }

    });
}

app.UseCors("AllowSpecificOrigins");
app.UseRequestContextLogging();
app.UseSerilogRequestLogging();
app.UseCustomExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

