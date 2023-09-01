using Serilog;
using Xunit.Abstractions;
using Xunit.Sdk;
using DotNet.Testcontainers.Containers;
using CleanArchitecture.Infastructure.Test.Helper;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infastructure.Common;
using CleanArchitecture.Infastructure.Repositories;

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
            DatabaseHelper.DeployDatabase(_dbConnectionModel, _dacpacFile, Log.Logger);

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
            Assert.Equal(name, storages[0].Name);
            Assert.Equal(description, storages[0].Description);


        }


        [Fact]
        public async Task GetAll_Returns_EmptyList()
        {
            //Arrange
            IUnitofWork _unitOfWork = new UnitOfWork(new DBConnectionFactory(_dbConnectionModel));

            //Act
            var queryStorages = await _unitOfWork.StorageRepository.GetAll();

            //Assert 
            Assert.Empty(queryStorages);

        }


        [Theory]
        [InlineData("FirstName", "ChangedName")]
        public async Task Create_Update_ValidData_Returns_ChangedArticle(string FirstStorageName, string ChangedStorageName)
        {
            //Arrange
            IUnitofWork _unitOfWork = new UnitOfWork(new DBConnectionFactory(_dbConnectionModel));
            var storageresult =  Storage.Create(Guid.NewGuid(), FirstStorageName, "");
            var _dummyStorage = storageresult.Result;


            //Act
            await _unitOfWork.StorageRepository.Add(_dummyStorage!);
            _unitOfWork.Commit();
            _dummyStorage!.UpdateStorageName(ChangedStorageName);

            await _unitOfWork.StorageRepository.Update(_dummyStorage!);
            _unitOfWork.Commit();
            var queryStorage = await _unitOfWork.StorageRepository.GetAll();


            //Assert
            Assert.NotNull(queryStorage);
            Assert.Equal(queryStorage[0].Name, ChangedStorageName);


        }


        [Theory]
        [InlineData("TestStorage")]
        public async Task Create_Delete_Returns_EmptyList(string FirstStorageName)
        {
            //Arrange
            IUnitofWork _unitOfWork = new UnitOfWork(new DBConnectionFactory(_dbConnectionModel));
            var storageresult = Storage.Create(Guid.NewGuid(), FirstStorageName, "");
            var _dummyStorage = storageresult.Result;


            //Act
            await _unitOfWork.StorageRepository.Add(_dummyStorage!);
            _unitOfWork.Commit();
            var queryList = await _unitOfWork.StorageRepository.GetAll();
            await _unitOfWork.StorageRepository.Delete(_dummyStorage!);
            _unitOfWork.Commit();
            queryList = await _unitOfWork.StorageRepository.GetAll();


            //Assert
            Assert.Empty(queryList);

        }

    }



    
}
