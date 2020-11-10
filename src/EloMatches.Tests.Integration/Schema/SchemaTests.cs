using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EloMatches.Infrastructure.Persistence;
using EloMatches.Query.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;
using Serilog.Extensions.Logging;

namespace EloMatches.Tests.Integration.Schema
{
    [TestFixture, Category("Schema")]
    public class SchemaTests
    {
        private string[] _connectionStrings;

        [OneTimeSetUp]
        public void SetUpOnce()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettingsTest.json", optional: false, reloadOnChange: false)
                .Build();

            _connectionStrings = configuration.GetSection("SchemaConnectionStrings").Get<string[]>();
        }

        [Test]
        public async Task CommandSchemaTest()
        {
            foreach(var connectionString in _connectionStrings)
                await PerformSchemaTest<EloMatchesCommandContext>(connectionString);
        }

        [Test]
        public async Task QuerySchemaTest()
        {
            foreach (var connectionString in _connectionStrings)
                await PerformSchemaTest<EloMatchesQueryContext>(connectionString);
        }

        private async Task PerformSchemaTest<TContext>(string connectionString) where TContext : DbContext
        {
            var contextType = typeof(TContext);

            var logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Debug().CreateLogger();
            var builder = new DbContextOptionsBuilder<TContext>();
            builder.UseSqlServer(connectionString);
            builder.UseLoggerFactory(new SerilogLoggerFactory(logger));
            builder.EnableSensitiveDataLogging();

            var options = builder.Options;

            var constructorAcceptingBuildOptions = typeof(TContext).GetConstructor(new[] { options.GetType() });

            if (constructorAcceptingBuildOptions == null)
                throw new ArgumentNullException(nameof(constructorAcceptingBuildOptions), $"Didn't find a constructor on type = '{contextType.Name}' that has BuildOptions accepting type = '{options.GetType().Name}'");

            await using var context = (TContext)constructorAcceptingBuildOptions.Invoke(new object[] { options });

            var dbSetProperty = context.GetType().GetMethod("Set", new Type[] { });

            if (dbSetProperty == null)
                throw new ArgumentNullException(nameof(dbSetProperty), "DbSet Property not found on DbContext");

            var entityTypes = context.Model.GetEntityTypes().Where(x => x.IsOwned() == false);

            foreach (var entityType in entityTypes)
            {
                var dbSet = (IQueryable<object>)dbSetProperty.MakeGenericMethod(entityType.ClrType).Invoke(context, null);

                if (dbSet == null)
                    throw new ArgumentNullException(nameof(dbSet), $"Could not create db set from entity type = '{entityType.ClrType.Name}'");

                _ = dbSet.FirstOrDefault(); //Query first entry to verify the schema is correct. Error will bubble up if the database schema does not match the CLR type.
            }

            Assert.Pass();
        }
    }
}