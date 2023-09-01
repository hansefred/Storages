using CleanArchitecture.Infastructure.Common;
using Microsoft.SqlServer.Dac;
using Serilog;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace CleanArchitecture.Infastructure.Test.Helper
{
    public static class DatabaseHelper
    {
        public static void DeployDatabase(IDBConnectionModel dBConfig, string DacFile, ILogger logger)
        {
            logger.Information("Connect to Database with Connection String: {ConnectionString}", dBConfig.GetConnectionString());
                var instance = new DacServices(dBConfig.GetConnectionString());

            logger.Information("Loading DAC File");
                using (var dacpac = DacPackage.Load(DacFile))
                {
                logger.Information("Deploy DAC File");
                instance.Deploy(dacpac, dBConfig.GetDBName());
                }


            

        }
        public static void WaitforSQLDB(string ConnectingString, ILogger logger, int Port = 1433, int Retry = 100)
        {
            int Start = ConnectingString.IndexOf("Server=") + 7;
            int End = ConnectingString.IndexOf(";", ConnectingString.IndexOf("Server=")) - (ConnectingString.IndexOf("Server=") + 7);

            string Server = ConnectingString.Substring(Start, End);

            int i = 1;

            while (i < Retry)
            {
                logger.Information($"Try to connect to Server: {Server}... ({i} - {Retry})");
                Ping PingSender = new Ping();
                try
                {
                    var Reply = PingSender.Send(Server);

                    if (Reply.Status == IPStatus.Success)
                    {
                        logger.Information($"Ping Sucess, try to connect to Port: {Port}... ({i})");
                        using (TcpClient tcpClient = new TcpClient())
                        {
                            try
                            {
                                tcpClient.Connect(Reply.Address.ToString(), Port);
                                logger.Information("Port open");
                                Thread.Sleep(TimeSpan.FromSeconds(15));
                                return;
                            }
                            catch
                            {
                                logger.Information("Port closed Wait and Try again... ");
                                Thread.Sleep(4000);
                                i++;
                            }
                        }
                    }
                }
                catch
                {
                    logger.Information("Wait and Try again... ");
                    Thread.Sleep(500);
                    i++;
                }


            }
        }


    }
}
