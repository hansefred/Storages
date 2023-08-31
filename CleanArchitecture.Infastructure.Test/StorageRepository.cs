using Serilog.Extensions.Logging;
using Serilog;
using Xunit.Abstractions;
using Xunit.Sdk;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Infastructure.Test.Helper;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infastructure.Common;
using CleanArchitecture.Infastructure.Repositories;
using System.Formats.Tar;

namespace CleanArchitecture.Infastructure.Test
{


    [Collection("Non-Parallel Collection")]
    public class StorageRepositoryTest : IAsyncLifetime
    {

        private IDBConnectionModel _dbConnectionModel;
        private string _dacpacFile;


        private readonly TestOutputHelper _logOutput;
        private IContainer? _dockerContainer;


        public StorageRepositoryTest(ITestOutputHelper logOutput)
        {
            _logOutput = (TestOutputHelper)logOutput;
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.TestOutput(logOutput)
            .CreateLogger();

            var solutionDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;
            _dacpacFile = Path.Combine(solutionDir ?? "", "StorageDatabase\\bin\\Debug\\StorageDatabase.dacpac");

            _dbConnectionModel = new DBConnectionModel("localhost", "TestDB", "sa", Guid.NewGuid().ToString());
        }

   




        public async Task DisposeAsync()
        {
            if (_dockerContainer is not null)
            {
                await _dockerContainer.StopAsync();
            }
        }

        public async Task InitializeAsync()
        {
            _dockerContainer = await DockerHelper.CreateDockerDatabase(_dbConnectionModel);
            DatabaseHelper.WaitforSQLDB(_dbConnectionModel.GetConnectionString(), Log.Logger);
            DatabaseHelper.DeployDatabase(_dbConnectionModel, _dacpacFile);

        }




        [Theory]
        [InlineData("FirstName", "FirstDescription")]
        public async Task Create_ValidValues_ReturnsNothing(string name, string description)
        {
            //Arrange
            IUnitofWork _unitOfWork = new UnitOfWork(new DBConnectionFactory(_dbConnectionModel));
            var result = Storage.Create(Guid.NewGuid(), name, description);
            var storage = result.Result;


            //Act
            await _unitOfWork.StorageRepository.Add(storage!);
            _unitOfWork.Commit();


            //Assert
            var storages = await _unitOfWork.StorageRepository.GetAll();
            Assert.Single(storages);


        }


        //[Fact]
        //public async Task ArticleRepository_GetAll_Returns_EmptyList()
        //{
        //    //Arrange
        //    IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());

        //    //Act
        //    var queryStorages = await _refStorageRepository.GetAll();

        //    //Assert 
        //    Assert.Empty(queryStorages);

        //}


        //[Theory]
        //[InlineData("FirstName", "ChangedName")]
        //public async Task ArticleRepository_Create_Update_Returns_ChangedArticle(string FirstStorageName, string ChangedStorageName)
        //{
        //    //Arrange
        //    IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
        //    Storage _dummyStorage = new Storage() { StorageName = FirstStorageName };


        //    //Act
        //    var storage = await _refStorageRepository.CreateStorage(_dummyStorage);
        //    storage.StorageName = ChangedStorageName;

        //    await _refStorageRepository.UpdateStorage(storage);
        //    var queryStorage = await _refStorageRepository.GetStoragebyID(storage.ID);


        //    //Assert
        //    Assert.NotNull(queryStorage);
        //    Assert.Equal(queryStorage.StorageName, ChangedStorageName);


        //}


        //[Theory]
        //[InlineData("TestStorage")]
        //public async Task ArticleRepository_Create_Delete_Returns_EmptyList(string FirstStorageName)
        //{
        //    //Arrange
        //    IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
        //    Storage _dummyStorage = new Storage() { StorageName = FirstStorageName };


        //    //Act
        //    var storage = await _refStorageRepository.CreateStorage(_dummyStorage);
        //    await _refStorageRepository.DeleteStorage(storage.ID);
        //    var queryStorage = await _refStorageRepository.GetAll();


        //    //Assert
        //    Assert.Empty(queryStorage);

        //}

        //[Theory]
        //[InlineData("TestStorage")]
        //public async Task StorageRepository_Create_ReturnsbyID_SingleArticle(string StorageName)
        //{
        //    //Arrange
        //    IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
        //    Storage _dummyStorage = new Storage() { StorageName = StorageName };


        //    //Act
        //    var storage = await _refStorageRepository.CreateStorage(_dummyStorage);

        //    var querybyID = await _refStorageRepository.GetStoragebyID(storage.ID);

        //    //Assert 
        //    Assert.NotNull(querybyID);

        //}
    }



    
}
