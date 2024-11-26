using Microsoft.EntityFrameworkCore;
using NadinSoft.Application;
using NadinSoft.Persistence;
using NadinSoft.Persistence.Data;
using NadinfSoft.Identity;
using NadinfSoft.Identity.Data;
using NadinSoft.Presentation.Configuration;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationDependencies()
                .AddPersistenceDependencies(builder.Configuration)
                .AddIdentityServices(builder.Configuration)
                .AddSwagger()
                .AddHttpConetxt();

builder.Services.AddResponseCompression(options => 
{
        options.Providers.Add<GzipCompressionProvider>();
        options.EnableForHttps = true;  
});

builder.Services.Configure<GzipCompressionProviderOptions>(o => o.Level = System.IO.Compression.CompressionLevel.SmallestSize);

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

