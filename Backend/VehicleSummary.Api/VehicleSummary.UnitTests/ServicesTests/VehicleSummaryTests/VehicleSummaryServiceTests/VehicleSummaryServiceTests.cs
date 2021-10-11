using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleSummary.Api.Services.VehicleSummary;
using Xunit;

namespace VehicleSummary.UnitTests.ServicesTests.VehicleSummaryTests.VehicleSummaryServiceTests
{
    public class VehicleSummaryServiceTests
    {
        [Fact]
        public async Task GetSummaryByMakeAsync_WithExistingMake_ReturnsExpectedMake()
        {
            // Arrange
            var vehicleApiModelsService = new Mock<IVehicleApiModelsService>();
            var vehicleApiModelsResponse = new VehicleApiModelsResponse
            {
                Make = "Tesla",
                Models = new Dictionary<string, List<int>>
                {
                    { "Model S", new List<int> { 2001, 2002, 2003} },
                    { "Model X", new List<int> { 2004, 2005, 2006, 2007} },
                }
            };
            var expectedVehicleSummaryResponse = new VehicleSummaryResponse
            {
                Make = "Tesla",
                Models = new List<VehicleSummaryModels>
                {
                    new VehicleSummaryModels { Name="Model S", YearsAvailable=3 },
                    new VehicleSummaryModels { Name="Model X", YearsAvailable=4 },
                }
            };

            vehicleApiModelsService.Setup(a => a.GetConsolidatedVehicleModelsAsync(It.IsAny<string>()))
                .ReturnsAsync(vehicleApiModelsResponse);
            var sut = new VehicleSummaryService(vehicleApiModelsService.Object);

            // Act
            var result = await sut.GetSummaryByMakeAsync(vehicleApiModelsResponse.Make);

            // Assert
            result.Should().BeEquivalentTo(expectedVehicleSummaryResponse);
        }

        [Fact]
        public async Task GetSummaryByMakeAsync_WithNonExistingMake_ReturnsNull()
        {
            // Arrange
            var vehicleApiModelsService = new Mock<IVehicleApiModelsService>();
            var make = "BMW";

            vehicleApiModelsService.Setup(a => a.GetConsolidatedVehicleModelsAsync(It.IsAny<string>()))
                .ReturnsAsync((VehicleApiModelsResponse)null);
            var sut = new VehicleSummaryService(vehicleApiModelsService.Object);

            // Act
            var result = await sut.GetSummaryByMakeAsync(make);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetSummaryByMakeAsync_MakeIsNullOrWhiteSpace_ThrowsArgumentNullException()
        {
            // Arrange
            var vehicleApiModelsService = new Mock<IVehicleApiModelsService>();
            var make = string.Empty;
            var sut = new VehicleSummaryService(vehicleApiModelsService.Object);

            // Act
            Func<Task> act = async () => await sut.GetSummaryByMakeAsync(make);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
