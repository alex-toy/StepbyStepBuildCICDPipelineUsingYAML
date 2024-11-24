using BookLibrary;
using BookLibrary.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldApp;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        string DefaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        builder.Services.AddDbContext<BooksContext>(options => options.UseSqlServer(DefaultConnectionString, b => b.MigrationsAssembly("HelloWorldApp")));
        builder.Services.AddDbContext<BloggingContext>(options => options.UseSqlServer(DefaultConnectionString, b => b.MigrationsAssembly("HelloWorldApp")));
    }
}
