using Microsoft.EntityFrameworkCore;
using NadinSoft.Application;
using NadinSoft.Persistence;
using NadinSoft.Persistence.Data;
using NadinSoft.Presentation.Configuration.Extensions.Swagger;
using NadinfSoft.Identity;
using NadinfSoft.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationDependencies()
                .AddPersistenceDependencies(builder.Configuration)
                .AddIdentityServices(builder.Configuration)
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

        using(var context = scope.ServiceProvider.GetService<UserDbContext>())
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

app.UseIdentity();
app.UseHttpsRedirection();
app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();

