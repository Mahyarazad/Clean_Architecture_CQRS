using Microsoft.EntityFrameworkCore;
using NadinSoft.Application;
using NadinSoft.Persistence;
using NadinSoft.Persistence.Data;
using NadinfSoft.Identity;
using NadinfSoft.Identity.Data;
using NadinSoft.Presentation.Configuration;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationDependencies()
                .AddPersistenceDependencies(builder.Configuration)
                .AddIdentityServices(builder.Configuration)
                .AddSwagger()
                .AddHttpConetxt()
                ;
                //.AddStackExchangeRedisCache(options =>
                //{
                //    options.Configuration = builder.Configuration.GetConnectionString("Redis");
                //    options.InstanceName = "Session";
                //}).Configure<LoggerFilterOptions>(options => options.AddFilter("StackExchange.Redis", LogLevel.Trace)); ;

builder.Services.AddResponseCompression(options => 
{
        options.Providers.Add<GzipCompressionProvider>();
        //options.EnableForHttps = true;  
});

builder.Services.Configure<GzipCompressionProviderOptions>(o => o.Level = System.IO.Compression.CompressionLevel.SmallestSize);
builder.Services.AddCors(options =>
{
    options.AddPolicy("default", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();

    });
});
var app = builder.Build();

//app.UseSession();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerDocumentation();
    using(var scope = app.Services.CreateScope())
    {
        using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>()) 
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
app.UseCors("default");

app.MapControllers();

app.Use(async (context, next) =>
{

    await next(context);

    // Log response details
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

app.Run();

