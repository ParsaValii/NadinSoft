using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Infrastructure;

namespace NadinSoft.Presentation.ExtentionMethods
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<NadinDbContext>();

                if (!context.Database.CanConnect())
                {
                    context.Database.EnsureCreated();
                }
                else
                {
                    // Check if any tables exist in the database
                    var tablesExist = context.Database.ExecuteSqlRaw("SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'");
                    if (tablesExist == 0)
                    {
                        context.Database.Migrate();
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while checking and creating the database.");
            }
        }
    }
    }
}