using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleSummary.Api.Controllers;
using VehicleSummary.Api.Services.VehicleSummary;
using Xunit;

namespace VehicleSummary.UnitTests.ControllersTests.VehicleChecksControllerTests
{
    public class VehicleChecksControllerTests
    {
        public VehicleChecksControllerTests()
        {
        }

        [Fact]
        public async Task GetMakes_WithEmptyMakeParameter_ReturnsBadRequest()
        {
            // Arrange
            var make = string.Empty;
            var vehicleSummaryService = new Mock<IVehicleSummaryService>();
            vehicleSummaryService.Setup(a => a.GetSummaryByMakeAsync(It.IsAny<string>()))
                .ReturnsAsync((VehicleSummaryResponse)null);
            var sut = new VehicleChecksController(vehicleSummaryService.Object);

            // Act
            var result = await sut.GetMakesAsync(make);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetMakes_WithNonExistingMake_ReturnsNotFound()
        {
            // Arrange
            var make = "Ferrari";
            var vehicleSummaryService = new Mock<IVehicleSummaryService>();
            vehicleSummaryService.Setup(a => a.GetSummaryByMakeAsync(It.IsAny<string>()))
                .ReturnsAsync((VehicleSummaryResponse)null);
            var sut = new VehicleChecksController(vehicleSummaryService.Object);

            // Act
            var result = await sut.GetMakesAsync(make);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetMakesAsync_WithExistingVehicleMake_ReturnsExpectedVehicleSummaryResponse()
        {
            // Arrange
            var make = "Tesla";
            var vehicleSummaryService = new Mock<IVehicleSummaryService>();
            var sut = new VehicleChecksController(vehicleSummaryService.Object);
            var expectedVehicleSummary = new VehicleSummaryResponse
            {
                Make = make,
                Models = new List<VehicleSummaryModels>
                {
                    new VehicleSummaryModels { Name="Model 3", YearsAvailable=3 },
                    new VehicleSummaryModels { Name="Model S", YearsAvailable=4 },
                    new VehicleSummaryModels { Name="Model Y", YearsAvailable=6 },
                    new VehicleSummaryModels { Name="Model X", YearsAvailable=5 }
                }
            };
            vehicleSummaryService.Setup(a => a.GetSummaryByMakeAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedVehicleSummary);

            // Act
            var result = await sut.GetMakesAsync(make);

            // Assert
            result.Value.Should().BeEquivalentTo(expectedVehicleSummary);
        }
    }
}