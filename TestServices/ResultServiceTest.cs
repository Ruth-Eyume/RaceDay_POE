using Microsoft.EntityFrameworkCore;
using Xunit;
using RaceDayAPI.Data;
using RaceDay.API.Models;
using RaceDay.API.Services;

namespace RaceDayAPI.Tests
{
    public class ResultServiceTest
    {
        private RaceDayDBContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new RaceDayDBContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllResults()
        {
            // Arrange
            using var context = CreateContext();

            context.Results.AddRange(
                new Result
                {
                    ResultID = 1,
                    EnrollmentID = 1,
                    FinishTime = TimeSpan.FromHours(2),
                    Position = 1,
                    ResultStatus = "Finished",
                    RecordedAt = DateTime.Now
                },
                new Result
                {
                    ResultID = 2,
                    EnrollmentID = 2,
                    FinishTime = TimeSpan.FromHours(3),
                    Position = 2,
                    ResultStatus = "Finished",
                    RecordedAt = DateTime.Now
                });

            await context.SaveChangesAsync();

            var service = new ResultService(context);

            // Act
            var results = await service.GetAllAsync();

            // Assert
            Assert.Equal(2, results.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsResult_WhenResultExists()
        {
            // Arrange
            using var context = CreateContext();

            context.Results.Add(new Result
            {
                ResultID = 1,
                EnrollmentID = 1,
                FinishTime = TimeSpan.FromHours(2),
                Position = 1,
                ResultStatus = "Finished",
                RecordedAt = DateTime.Now
            });

            await context.SaveChangesAsync();

            var service = new ResultService(context);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ResultID);
            Assert.Equal("Finished", result.ResultStatus);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenResultDoesNotExist()
        {
            // Arrange
            using var context = CreateContext();
            var service = new ResultService(context);

            // Act
            var result = await service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenResultExists()
        {
            // Arrange
            using var context = CreateContext();

            context.Results.Add(new Result
            {
                ResultID = 1,
                EnrollmentID = 1,
                FinishTime = TimeSpan.FromHours(2),
                Position = 1,
                ResultStatus = "Finished",
                RecordedAt = DateTime.Now
            });

            await context.SaveChangesAsync();

            var service = new ResultService(context);

            // Act
            var deleted = await service.DeleteAsync(1);

            // Assert
            Assert.True(deleted);
            Assert.Empty(await context.Results.ToListAsync());
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenResultDoesNotExist()
        {
            // Arrange
            using var context = CreateContext();
            var service = new ResultService(context);

            // Act
            var deleted = await service.DeleteAsync(999);

            // Assert
            Assert.False(deleted);
        }
    }
}


