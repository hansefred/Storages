using CleanArchitecture.Infastructure.Common;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Serilog;

namespace CleanArchitecture.Infastructure.Test.Helper
{
    public static class DockerHelper
    {
        public static async Task<IContainer?> CreateDockerDatabase(IDBConnectionModel dBConfig)
        {
            try
            {
                var dockerContainer = new ContainerBuilder()
                    .WithName(Guid.NewGuid().ToString("D"))
                    .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                    .WithPortBinding(1433, 1433)
                    .WithEnvironment(new Dictionary<string, string>()
                    {
                                { "ACCEPT_EULA", "Y" },
                                { "MSSQL_SA_PASSWORD", dBConfig.GetPassword()}
                    })
                .Build();
                await dockerContainer.StartAsync();
                
                return dockerContainer;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Cannot create Docker SQL Database: {ex}", ex);
                return null;
            }
        }
    }
}
