using BookLibrary;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldApp;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        string DefaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        builder.Services.AddDbContext<BooksContext>(options => options.UseSqlServer(DefaultConnectionString, b => b.MigrationsAssembly("HelloWorldApp")));
    }
}
