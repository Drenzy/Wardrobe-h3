namespace WardrobeTests.Repositories
{
    public class ClosetReporsitoryTest
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly ClosetReporsitory _closetReporsitory;

        public ClosetReporsitoryTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "ClosetReporsitoryTest")
                .Options;

            _context = new(_options);

            _closetReporsitory = new(_context);
        }

        // TestMethods. //
        [Fact]
        public async void GetAllAsync_ShouldReturnListOfAperals_WhenClosetExists()
        {
            //Arrange
            // Makes sure the database is deleted
            await _context.Database.EnsureDeletedAsync();

            // Adding data into context which is a "copy" of the database
            _context.Closet.Add(new Closet
            {
                Id = 1,
                Name = "Test"
            });
            _context.Closet.Add(new Closet
            {
                Id = 2,
                Name = "Test"
            });

            // Saves the added changes 
            await _context.SaveChangesAsync();

            // Act
            // Waiting for the task to be completed
            var result = await _closetReporsitory.GetAllAsync();

            //assert
            // Makes sure it is NotNUll
            Assert.NotNull(result);
            // Makes sure its the right type it is given
            Assert.IsType<List<Closet>>(result);
            // Verifying that the two the objects are eaquel / making sure there are 2 entries in the database
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfClosets_WhenNoClosetExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _closetReporsitory.GetAllAsync();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<Closet>>(result);
            // Veryfies that the collection is empty
            Assert.Empty(result);
        }

        [Fact]
        public async void CreateAsync_ShouldAddNewIdToCloset_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedId = 1;

            Closet closet = new()
            {
                Name = "Test"
            };

            //Act
            var result = await _closetReporsitory.CreateAsync(closet);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Closet>(result);
            // Verifying that the expected ID is the same as the result ID.
            Assert.Equal(expectedId, result?.Id);
        }

        [Fact]
        public async void CreateAsync_ShouldFailToAddNewCloset_WhenClosetIdAlreadyExists()
        {
            await _context.Database.EnsureDeletedAsync();


            Closet closet = new()
            {
                Name= "Test"
            };
            await _closetReporsitory.CreateAsync(closet);
            //Act
            async Task action() => await _closetReporsitory.CreateAsync(closet);
            //Assert
            // Verifying that the error message is outputed
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnCloset_WhenClosetExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int closetId = 1;

            _context.Closet.Add(new()
            {
                Id = closetId,
                Name= "Test"
            });

            //Act
            var reusult = await _closetReporsitory.FindByIdAsync(closetId);


            //Assert
            Assert.NotNull(reusult);
            Assert.Equal(closetId, reusult.Id);

        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenClosetlDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int apparelId = 1;

            //Act
            var reusult = await _closetReporsitory.FindByIdAsync(apparelId);

            // Assert
            Assert.Null(reusult);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnUpdatedCloset_WhenClosetExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int closetId = 1;

            _context.Closet.Add(new()
            {
                Id = closetId,
                Name= "Test"
            });

            await _context.SaveChangesAsync();

            Closet updateCloset = new()
            {
                Name= "Tes3"
            };

            var result = await _closetReporsitory.UpdateByIdAsync(closetId, updateCloset);

            Assert.NotNull(result);
            Assert.IsType<Closet>(result);
            Assert.Equal(updateCloset.Name, result.Name);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnNull_WhenClosetDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int closetId = 1;

            Closet updateCloset = new()
            {
                Name = "Rettet Skab"
            };
            var result = await _closetReporsitory.UpdateByIdAsync(closetId, updateCloset);

            Assert.Null(result);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnCloset_WhenClosetIsDeleted()
        {
            await _context.Database.EnsureDeletedAsync();

            int closetId = 1;

            Closet closet = new()
            {
                Name = "TEST"
            };

            await _closetReporsitory.CreateAsync(closet);

            var result = await _closetReporsitory.DeleteByIdAsync(closetId);

            Assert.NotNull(result);
            Assert.IsType<Closet>(result);
            Assert.Equal(closetId, result.Id);
            Assert.Equal(closet.Name, result.Name);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnNull_WhenClosetDoesNotExist()
        {
            await _context.Database.EnsureDeletedAsync();

            int closetId = 1;

            var result = await _closetReporsitory.DeleteByIdAsync(closetId);

            Assert.Null(result);
        }
    }
}
