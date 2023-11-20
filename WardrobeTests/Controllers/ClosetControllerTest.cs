namespace WardrobeTests.Controllers
{
    public class ClosetControllerTest
    {
        private readonly ClosetController _closetController;
        private readonly Mock<IClosetReporsitory> _closetRepositoryMOCK = new();

        public ClosetControllerTest()
        {
            _closetController = new(_closetRepositoryMOCK.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnStatusCode200_WhenClosetsExists()
        {
            // Making a list of the data in our database
            List<Closet> closets = new()
            {
                new Closet
                {
                    Id= 1,
                    Name= "Test"
                },
                new Closet
                {
                    Id= 2,
                    Name= "Test"
                },
            };
            // Returns the apparels list
            _closetRepositoryMOCK.Setup(a => a.GetAllAsync()).ReturnsAsync(closets);

            //var
            var result = await _closetController.GetAllAsync();

            //Assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            //Returns the status code 200 if true
            Assert.Equal(200, objectResult.StatusCode);

            Assert.NotNull(objectResult.Value);
            // verying that the list apparel is the right type
            Assert.IsType<List<ClosetResponse>>(objectResult.Value);

            // insert the objectResult into List
            var data = objectResult.Value as List<ClosetResponse>;
            Assert.NotNull(data);
            Assert.Equal(2, data.Count);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {


            _closetRepositoryMOCK.Setup(a => a.GetAllAsync())
                .ReturnsAsync(() => throw new Exception("This is an expection"));

            //var
            var result = await _closetController.GetAllAsync();

            //Assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);

            var data = objectResult.Value as List<ClosetResponse>;
            Assert.Null(data);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnStatusCode200_WhenClosetIsSuccessfullyCreated()
        {
            //arrange
            ClosetRequset closetRequset = new()
            {
                Name= "name"
            };

            int closetId = 1;
            Closet closet = new()
            {
                Id = closetId,
                Name= "name"
            };

            _closetRepositoryMOCK
                .Setup(x => x.CreateAsync(It.IsAny<Closet>()))
                .ReturnsAsync(closet);
            //act
            var result = await _closetController.CreateAsync(closetRequset);
            //assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var data = objectResult.Value as ClosetResponse;
            Assert.NotNull(data);
            Assert.Equal(closetId, data.Id);
            Assert.Equal(closet.Name, data.Name);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //arange
            ClosetRequset closetRequset = new()
            {
                Name = "Den blå skjorte"
            };
            _closetRepositoryMOCK
                .Setup(x => x.CreateAsync(It.IsAny<Closet>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));
            //act
            var result = await _closetController.CreateAsync(closetRequset);
            //assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode200_WhenClosetExists()
        {
            // Arrange
            int closetId = 1;
            ClosetResponse closetResponse = new()
            {
                Id = closetId,
                Name = "name"
            };

            Closet closet = new()
            {
                Id = closetId,
                Name = "name"
            };
            _closetRepositoryMOCK.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(closet);

            // Act
            var result = await _closetController.FindByIdAsync(closetId);

            // Assert
            var obejctReuslt = result as ObjectResult;
            Assert.NotNull(obejctReuslt);
            Assert.Equal(200, obejctReuslt.StatusCode);

            var data = obejctReuslt.Value as ClosetResponse;
            Assert.NotNull(data);
            Assert.Equal(closetId, data.Id);
            Assert.Equal(closetResponse.Name, data.Name);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode404_WhenApparelDoesNotExist()
        {
            int closetId = 1;

            _closetRepositoryMOCK.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var result = await _closetController.FindByIdAsync(closetId);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            int closetId = 1;

            _closetRepositoryMOCK.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(()
                => throw new Exception("This is an execption"));

            var result = await _closetController.FindByIdAsync(closetId);

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);

        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnStatusCode200_WhenClosetIsUpdated()
        {
            //arrange
            ClosetRequset closetRequset = new()
            {
                Name= "Test"
            };

            int closetId = 1;
            Closet closet = new()
            {
                Id = closetId,
                Name = "Test"
            };
            _closetRepositoryMOCK
                .Setup(x => x.UpdateByIdAsync(It.IsAny<int>(), It.IsAny<Closet>()))
                .ReturnsAsync(closet);


            //Act
            var result = await _closetController.UpdateByIdAsync(closetId, closetRequset);

            //Assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var data = objectResult.Value as ClosetResponse;
            Assert.NotNull(data);
            Assert.Equal(closetRequset.Name, data.Name);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnStatusCode404_WhenClosetDoesNotExist()
        {
            //Arrange
            ClosetRequset closetRequset = new()
            {
                Name = "Blå skjorte"
            };

            int closetId = 1;

            _closetRepositoryMOCK.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act
            var result = await _closetController.UpdateByIdAsync(closetId, closetRequset);

            // Assert
            var objetResult = result as NotFoundResult;
            Assert.NotNull(objetResult);
            Assert.Equal(404, objetResult.StatusCode);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // arrange 
            ClosetRequset closetRequset = new()
            {
                Name = "TEST"
            };

            int closetId = 1;

            _closetRepositoryMOCK.Setup(x => x.UpdateByIdAsync(It.IsAny<int>(), It.IsAny<Closet>())).ReturnsAsync(()
                => throw new Exception("This is an execption"));

            // Act
            var result = await _closetController.UpdateByIdAsync(closetId, closetRequset);

            // Assert
            var objetResult = result as ObjectResult;
            Assert.NotNull(objetResult);
            Assert.Equal(500, objetResult.StatusCode);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnStatusCode200_WhenClosetIsDeleted()
        {
            int closetId = 1;

            Closet closet = new()
            {
                Id = closetId,
                Name = "name"
            };

            _closetRepositoryMOCK
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(closet);

            //act
            var result = await _closetController.DeleteByIdAsync(closetId);

            //assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var data = objectResult.Value as ClosetResponse;
            Assert.NotNull(data);
            Assert.Equal(closetId, data.Id);
            Assert.Equal(closet.Name, data.Name);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnStatusCode404_WhenClosetDoesNotExist()
        {
            int closetId = 1;

            _closetRepositoryMOCK
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);


            //act
            var result = await _closetController.DeleteByIdAsync(closetId);

            //Assert
            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            int closetId = 1;

            _closetRepositoryMOCK
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));
        }
    }
}
