using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Test
{
    public class StorageEntity
    {
        [Theory]
        [InlineData("kitchen", "left cabinet below")]
        public void Create_ValidValues_ReturnsEntity(string name, string description)
        {
            // Arrange

            // Act
            var result = Storage.Create(Guid.NewGuid(), name, description);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Null(result.DomainException);
            Assert.Equal(name, result.Result.Name);
            Assert.Equal(description, result.Result.Description);
        }

        [Theory]
        [InlineData("", "left cabinet below")]
        public void Create_EmptyStringName_ReturnsError(string name, string description)
        {
            // Arrange

            // Act
            var result = Storage.Create(Guid.NewGuid(), name, description);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
            Assert.NotNull(result.DomainException);
            Assert.IsType<StorageNameIsOutofRangeDomainException>(result.DomainException);
        }

        [Theory]
        [InlineData("my nice kitchen with good and old parts i like the oven, it can boil meat so well", "left cabinet below")]
        public void Create_ToLongName_ReturnsError(string name, string description)
        {
            // Arrange

            // Act
            var result = Storage.Create(Guid.NewGuid(), name, description);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
            Assert.NotNull(result.DomainException);
            Assert.IsType<StorageNameIsOutofRangeDomainException>(result.DomainException);
        }


        [Theory]
        [InlineData("kitchen", "hello let me write a very long story about my life because i need to test the description attribute guard in this test via xunit on a sunday evening. I need more bla bla but have no more context :( sad live goodby")]
        public void Create_ToLongStringDescription_ReturnsError(string name, string description)
        {
            // Arrange

            // Act
            var result = Storage.Create(Guid.NewGuid(), name, description);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
            Assert.NotNull(result.DomainException);
            Assert.IsType<StorageDescriptionIsOutofRangeDomainException>(result.DomainException);
        }

        [Theory]
        [InlineData("kitchen", "")]
        public void Create_EmptyDescription_ReturnsEntity(string name, string description)
        {
            // Arrange

            // Act
            var result = Storage.Create(Guid.NewGuid(), name, description);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Null(result.DomainException);
            Assert.Equal(name, result.Result.Name);
            Assert.Equal(description, result.Result.Description);

        }



        [Theory]
        [InlineData("raw weed", "for making some cakes")]
        public void AddArticleToStorage_ValidValues_ReturnEntity(string articleName, string articleDescription)
        {
            //Arrange
            var storage = Storage.Create(Guid.NewGuid(), "storagename", "storagedescription").Result!;

            //Act
            var result = storage.AddArticleToStorage(Guid.NewGuid(), articleName, articleDescription);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.DomainException);
            Assert.NotNull(result.Result);
            Assert.NotNull(result.Result.StorageArticles.First());

            var article = result.Result.StorageArticles.First();
            Assert.Equal(articleName, article.ArticleName);
            Assert.Equal(articleDescription, article.Description);
            Assert.Equal(storage, article.Storage);
        }

        [Theory]
        [InlineData("raw weed", "")]
        public void AddArticleToStorage_EmptyArticleDescription_ReturnEntity(string articleName, string articleDescription)
        {
            //Arrange
            var storage = Storage.Create(Guid.NewGuid(), "storagename", "storagedescription").Result!;

            //Act
            var result = storage.AddArticleToStorage(Guid.NewGuid(),articleName, articleDescription);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.DomainException);
            Assert.NotNull(result.Result);
            Assert.NotNull(result.Result.StorageArticles.First());

            var article = result.Result.StorageArticles.First();
            Assert.Equal(articleName, article.ArticleName);
            Assert.Equal(articleDescription, article.Description);
            Assert.Equal(storage, article.Storage);
        }

        [Theory]
        [InlineData("weed", "for making some cakes")]
        public void AddArticleToStorage_ArticleNameToShort_ReturnError(string articleName, string articleDescription)
        {
            //Arrange
            var storage = Storage.Create(Guid.NewGuid(), "storagename", "storagedescription").Result!;

            //Act
            var result = storage.AddArticleToStorage(Guid.NewGuid(),articleName, articleDescription);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.DomainException);
            Assert.Null(result.Result);
            Assert.IsType<StorageArticleNameIsOutofRangeDomainException>(result.DomainException);
        }

        [Theory]
        [InlineData("Wheat with a lot of flavor and well suited for soups and breads", "for making some cakes")]
        public void AddArticleToStorage_ArticleNameToLong_ReturnError(string articleName, string articleDescription)
        {
            //Arrange
            var storage = Storage.Create(Guid.NewGuid(), "storagename", "storagedescription").Result!;

            //Act
            var result = storage.AddArticleToStorage(Guid.NewGuid(), articleName, articleDescription);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.DomainException);
            Assert.Null(result.Result);
            Assert.IsType<StorageArticleNameIsOutofRangeDomainException>(result.DomainException);
        }

        [Theory]
        [InlineData("raw weed" , "you can bake very good bread with it, but is it suitable for pizzas, i love a lot of pepperoni and salami with extra cheese on the pizza. The best way to eat pizza is with your best friends on a nice summer day.")]
        public void AddArticleToStorage_ArticleDescriptionToLong_ReturnError(string articleName, string articleDescription)
        {
            //Arrange
            var storage = Storage.Create(Guid.NewGuid(), "storagename", "storagedescription").Result!;

            //Act
            var result = storage.AddArticleToStorage(Guid.NewGuid(),articleName, articleDescription);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.DomainException);
            Assert.Null(result.Result);
            Assert.IsType<StorageArticleDescriptionIsOutofRangeDomainException>(result.DomainException);
        }






    }
}