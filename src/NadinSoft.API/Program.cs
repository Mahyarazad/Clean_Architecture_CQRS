using Microsoft.EntityFrameworkCore;
using NadinSoft.Application;
using NadinSoft.Persistence;
using NadinSoft.Persistence.Data;
using NadinSoft.Presentation.Configuration.Extensions.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDependencies();
builder.Services.AddControllers();
builder.Services.AddPersistenceDependencies(builder.Configuration)
    .AddSwagger();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerDocumentation();
    using(var scope = app.Services.CreateScope())
    {
        using(var context = scope.ServiceProvider.GetService<ApplicationDbContext>()) 
        {
            context!.Database.Migrate();
            context!.Database.EnsureCreated();
        }
    }
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();

