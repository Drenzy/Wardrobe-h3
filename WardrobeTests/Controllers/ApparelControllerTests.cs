namespace WardrobeTests.Controllers
{
    public class ApparelControllerTests
    {
        private readonly ApparelController _apparelController;
        private readonly Mock<IApparelRepository> _apparelRepositoryMock = new();

        public ApparelControllerTests()
        {
            _apparelController = new(_apparelRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnStatusCode200_WhenApparelsExists() 
        {
            // Making a list of the data in our database
            List<Apparel> apparels = new()
            {
                new Apparel
                {
                    Id= 1,
                    Title = "Den blå skjorte",
                    Description = "Den blå skjorte med de hvide wrangler",
                    Color = "blå"
                },
                new Apparel
                {
                    Id= 2,
                    Title = "Sorte Jeans",
                    Description = "De sorte wrangler",
                    Color = "Sort"
                },
            };
            // Returns the apparels list
            _apparelRepositoryMock.Setup(a => a.GetAllAsync()).ReturnsAsync(apparels);

            //var
            var result = await _apparelController.GetAllAsync();

            //Assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            //Returns the status code 200 if true
            Assert.Equal(200, objectResult.StatusCode);

            Assert.NotNull(objectResult.Value);
            // verying that the list apparel is the right type
            Assert.IsType<List<ApparelResponse>>(objectResult.Value);
            
            // insert the objectResult into List
            var data = objectResult.Value as List<ApparelResponse>;
            Assert.NotNull(data);
            Assert.Equal(2, data.Count);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            

            _apparelRepositoryMock.Setup(a => a.GetAllAsync())
                .ReturnsAsync(() => throw new Exception("This is an expection"));

            //var
            var result = await _apparelController.GetAllAsync();

            //Assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);

            var data = objectResult.Value as List<ApparelResponse>;
            Assert.Null(data);
        }
        [Fact]
        public async void CreateAsync_ShouldReturnStatusCode200_WhenApparelIsSuccessfullyCreated()
        {
            //arrange


            ApparelRequest apparelRequest = new()
            {
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "blå"
            };

            int apparelId = 1;
            Apparel apparel = new()
            {
                Id = apparelId,
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "blå"
            };

            _apparelRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Apparel>()))
                .ReturnsAsync(apparel);
            //act
            var result = await _apparelController.CreateAsync(apparelRequest);
            //assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var data = objectResult.Value as ApparelResponse;
            Assert.NotNull(data);
            Assert.Equal(apparelId, data.Id);
            Assert.Equal(apparel.Title, data.Title);
            Assert.Equal(apparel.Description, data.Description);
            Assert.Equal(apparel.Color, data.Color);
        }
        [Fact]
        public async void CreateAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //arange
            ApparelRequest apparelRequest = new()
            {
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "blå"
            };
            _apparelRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Apparel>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));
            //act
            var result = await _apparelController.CreateAsync(apparelRequest);
            //assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode200_WhenApparelExists()
        {
            // Arrange
            int apparelId = 1;
            ApparelResponse apparelResponse = new()
            {
                Id = apparelId,
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "blå"
            };

            Apparel apparel = new()
            {
                Id = apparelId,
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "blå"
            };
            _apparelRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(apparel);

            // Act
            var result = await _apparelController.FindByIdAsync(apparelId);

            // Assert
            var obejctReuslt = result as ObjectResult;
            Assert.NotNull(obejctReuslt);
            Assert.Equal(200, obejctReuslt.StatusCode);

            var data = obejctReuslt.Value as ApparelResponse;
            Assert.NotNull(data);
            Assert.Equal(apparelId, data.Id);
            Assert.Equal(apparelResponse.Title, data.Title);
            Assert.Equal(apparelResponse.Description, data.Description);
            Assert.Equal(apparelResponse.Color, data.Color);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode404_WhenApparelDoesNotExist()
        {
            int apparelId = 1;

            _apparelRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var result = await _apparelController.FindByIdAsync(apparelId);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            int apparelId = 1;

            _apparelRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(() 
                => throw new Exception("This is an execption"));

            var result = await _apparelController.FindByIdAsync(apparelId);

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);

        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnStatusCode200_WhenApparelIsUpdated()
        {
            //arrange
            ApparelRequest apparelRequest = new()
            {
                Title = "Blå skjorte",
                Description = "Den blå skjorte med hvide prikker",
                Color = "Blå"
            };

            int apparelId = 1;
            Apparel apparel = new()
            {
                Id = apparelId,
                Title = "Blå skjorte",
                Description = "Den blå skjorte med hvide prikker",
                Color = "Blå"
            };
            _apparelRepositoryMock
                .Setup(x => x.UpdateByIdAsync(It.IsAny<int>(), It.IsAny<Apparel>()))
                .ReturnsAsync(apparel);


            //Act
            var result = await _apparelController.UpdateByIdAsync(apparelId, apparelRequest);

            //Assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var data = objectResult.Value as ApparelResponse;
            Assert.NotNull(data);
            Assert.Equal(apparelRequest.Title, data.Title);
            Assert.Equal(apparelRequest.Description, data.Description);
            Assert.Equal(apparelRequest.Color, data.Color);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnStatusCode404_WhenApparelDoesNotExist()
        {
            //Arrange
            ApparelRequest apparelRequest = new()
            {
                Title = "Blå skjorte",
                Description = "Den blå skjorte med hvide prikker",
                Color = "Blå"
            };

            int apparelId = 1;

            _apparelRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act
            var result = await _apparelController.UpdateByIdAsync(apparelId, apparelRequest);

            // Assert
            var objetResult = result as NotFoundResult;
            Assert.NotNull(objetResult);
            Assert.Equal(404, objetResult.StatusCode);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // arrange 
            ApparelRequest apparelRequest = new()
            {
                Title = "Blå skjorte",
                Description = "Den blå skjorte med hvide prikker",
                Color = "Blå"
            };

            int apparelId = 1;

            _apparelRepositoryMock.Setup(x => x.UpdateByIdAsync(It.IsAny<int>(), It.IsAny<Apparel>())).ReturnsAsync(()
                => throw new Exception("This is an execption"));

            // Act
            var result = await _apparelController.UpdateByIdAsync(apparelId, apparelRequest);

            // Assert
            var objetResult = result as ObjectResult;
            Assert.NotNull(objetResult);
            Assert.Equal(500, objetResult.StatusCode);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnStatusCode200_WhenApparelIsDeleted()
        {
            int apparelId = 1;

            Apparel apparel = new()
            {
                Id = apparelId,
                Title = "Blå skjorte",
                Description = "Den blå skjorte med hvide prikker",
                Color = "Blå"
            };

            _apparelRepositoryMock
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(apparel);

            //act
            var result = await _apparelController.DeleteByIdAsync(apparelId);

            //assert
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

            var data = objectResult.Value as ApparelResponse;
            Assert.NotNull(data);
            Assert.Equal(apparelId, data.Id);
            Assert.Equal(apparel.Title, data.Title);
            Assert.Equal(apparel.Description, data.Description);
            Assert.Equal(apparel.Color, data.Color);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnStatusCode404_WhenApparelDoesNotExist()
        {
            int apparelId = 1;

            _apparelRepositoryMock
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);


            //act
            var result = await _apparelController.DeleteByIdAsync(apparelId);

            //Assert
            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
            Assert.Equal(404, objectResult.StatusCode);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            int apparelId = 1;

            _apparelRepositoryMock
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));
        }
    }
}
