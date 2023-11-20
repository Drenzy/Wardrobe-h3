namespace WardrobeTests.Repositories
{
    public class ApparelRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly ApparelRepository _apparelRepository;

        public ApparelRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "ApparelRepositoryTests")
                .Options;

            _context = new(_options);

            _apparelRepository = new(_context);

        }

        // TestMethods. //
        [Fact]
        public async void GetAllAsync_ShouldReturnListOfAperals_WhenApparelExists()
        {
            //Arrange
            // Makes sure the database is deleted
            await _context.Database.EnsureDeletedAsync();

            // Adding data into context which is a "copy" of the database
            _context.Apparel.Add(new Apparel
            {
                Id = 1,
                Title = "Den blå Skjorte",
                Description = "Den blå skjorte med hvide prikker",
                Color = "Blå"
            });
             _context.Apparel.Add(new Apparel
             {
                 Id = 2,
                 Title = "Sorte Jeans",
                 Description = "Det storte wrangler",
                 Color = "Sort"
             });

            // Saves the added changes 
            await _context.SaveChangesAsync();

            // Act
            // Waiting for the task to be completed
            var result = await _apparelRepository.GetAllAsync();

            //assert
            // Makes sure it is NotNUll
            Assert.NotNull(result);
            // Makes sure its the right type it is given
            Assert.IsType<List<Apparel>>(result);
            // Verifying that the two the objects are eaquel / making sure there are 2 entries in the database
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfApparels_WhenNoApparelExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();


            // Act
            var result = await _apparelRepository.GetAllAsync();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Apparel>>(result);
            // Veryfies that the collection is empty
            Assert.Empty(result);
        }

        [Fact]
        public async void CreateAsync_ShouldAddNewIdToApparel_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedId = 1;

            Apparel apparel = new()
            {
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "Blå"
            };

            //Act
            var result = await _apparelRepository.CreateAsync(apparel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Apparel>(result);
            // Verifying that the expected ID is the same as the result ID.
            Assert.Equal(expectedId, result?.Id);
        }

        [Fact]
        public async void CreateAsync_ShouldFailToAddNewApparel_WhenApparelIdAlreadyExists()
        {
            await _context.Database.EnsureDeletedAsync();


            Apparel apparel = new()
            {
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "Blå"
            };
            await _apparelRepository.CreateAsync(apparel);
            //Act
            async Task action() => await _apparelRepository.CreateAsync(apparel);
            //Assert
            // Verifying that the error message is outputed
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnApparel_WhenApparelExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int apparelId = 1;

            _context.Apparel.Add(new()
            {
                Id = apparelId,
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "Blå"
            });

            //Act
            var reusult = await _apparelRepository.FindByIdAsync(apparelId);


            //Assert
            Assert.NotNull(reusult);
            Assert.Equal(apparelId, reusult.Id);

        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenApparelDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int apparelId = 1;

            //Act
            var reusult = await _apparelRepository.FindByIdAsync(apparelId);

            // Assert
            Assert.Null(reusult);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnUpdatedApparel_WhenApparelExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int apparelId = 1;

            _context.Apparel.Add(new()
            {
                Id = apparelId,
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "Blå"
            });

            await _context.SaveChangesAsync();

            Apparel updateApparel = new()
            {
                Title = "Rettet Skjote",
                Description = "Den rettet skjorte med hvide prikker",
                Color = "Hvid"
            };

            var result = await _apparelRepository.UpdateByIdAsync(apparelId, updateApparel);

            Assert.NotNull(result);
            Assert.IsType<Apparel>(result);
            Assert.Equal(updateApparel.Title, result.Title);
            Assert.Equal(updateApparel.Description, result.Description);
            Assert.Equal(updateApparel.Color, result.Color);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnNull_WhenApparelDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int apparelId = 1;

            Apparel updateApparel = new()
            {
                Title = "Rettet Skjote",
                Description = "Den rettet skjorte med hvide prikker",
                Color = "Hvid"
            };
            var result = await _apparelRepository.UpdateByIdAsync(apparelId, updateApparel);

            Assert.Null(result);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnApparel_WhenApparelIsDeleted()
        {
            await _context.Database.EnsureDeletedAsync();

            int apparelId = 1;

            Apparel apparel = new()
            {
                Title = "Den blå skjorte",
                Description = "Den blå skjorte med de hvide wrangler",
                Color = "Blå"
            };

            apparel.Closet = new()
            {
                Id = 1,
                Name = "Test"
            };

            await _apparelRepository.CreateAsync(apparel);

            var result = await _apparelRepository.DeleteByIdAsync(apparelId);

            Assert.NotNull(result);
            Assert.IsType<Apparel>(result);
            Assert.Equal(apparelId, result.Id);
            Assert.Equal(apparel.Title, result.Title);
            Assert.Equal(apparel.Description, result.Description);
            Assert.Equal(apparel.Color, result.Color);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnNull_WhenApparelDoesNotExist()
        {
            await _context.Database.EnsureDeletedAsync();

            int apparelId = 1;

            var result = await _apparelRepository.DeleteByIdAsync(apparelId);

            Assert.Null(result);
        }
    }
}
