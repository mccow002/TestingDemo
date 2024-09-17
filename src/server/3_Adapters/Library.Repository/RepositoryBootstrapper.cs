using Library.Domain.Contracts;
using Library.Models.Contracts;
using Library.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Repository;

public static class RepositoryBootstrapper
{
    public static IHostApplicationBuilder AddRepository(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LibraryContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
        });
        
        builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        return builder;
    }
}