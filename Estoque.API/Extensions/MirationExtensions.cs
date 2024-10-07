using Estoque.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoque.API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using MicroServiceContext dbContext =
                scope.ServiceProvider.GetRequiredService<MicroServiceContext>();

            dbContext.Database.Migrate();
        }
    }
}
