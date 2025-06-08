using Infrastructure;
using API.Modules;
using Application;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder);
builder.Services.AddApplication();
builder.Services.SetupServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerDocumentation();
builder.Services.AddSwaggerAuth();
builder.Services.AddJwtTokenAuth(builder.Configuration);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ostroh Problems API V1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "Ostroh Problems API Documentation";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options => options
    .WithOrigins("http://localhost:5173")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

await app.InitialiseDb();  
app.MapControllers();

app.UseHttpsRedirection();

var imagesPath = Path.Combine(builder.Environment.ContentRootPath, "data/images");

if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/images"
});

app.Run();
