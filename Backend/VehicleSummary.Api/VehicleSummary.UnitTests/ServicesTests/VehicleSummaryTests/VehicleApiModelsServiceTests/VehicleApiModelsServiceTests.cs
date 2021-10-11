using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using Newtonsoft.Json;
using VehicleSummary.Api.Services.VehicleSummary;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using VehicleSummary.Api.Settings;

namespace VehicleSummary.UnitTests.ServicesTests.VehicleSummaryTests.VehicleSummaryServiceTests
{
    public class VehicleApiModelsServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly VehicleApiOptions _vehicleApiOptions;
        private readonly IConfigurationRoot _configuration;

        public VehicleApiModelsServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _vehicleApiOptions = configuration.GetSection("VehicleApiSettings").Get<VehicleApiOptions>();
            _configuration = configuration;
        }

        #region GetConsolidatedVehicleModelsAsync
        [Fact]
        public async Task GetConsolidatedVehicleModelsAsync_WithExistingMake_ReturnsExpectedMake()
        {
            // Arrange
            var sut = new VehicleApiModelsService(_configuration);
            var make = "Tesla";
            var expectedVehicleApiModelsResponse = new VehicleApiModelsResponse
            {
                Make = make,
                Models = new Dictionary<string, List<int>>
                {
                    { "Model 3", new List<int> { 2007, 2008, 2009 } },
                    { "Model S", new List<int> { 2010, 2011 } }
                }
            };

            VehicleApiModelsResponse vehicleApiModelsResponse;

            // Act
            using (var httpTest = new HttpTest())
            {
                var vehicleModels = expectedVehicleApiModelsResponse.Models.Keys;
                httpTest.RespondWithJson(vehicleModels);
                foreach (var model in expectedVehicleApiModelsResponse.Models)
                {
                    var yearOfModel = model.Value.ToArray();
                    httpTest.RespondWithJson(yearOfModel);
                }

                vehicleApiModelsResponse = await sut.GetConsolidatedVehicleModelsAsync(make);

                // Assert
                var expectedUrl =
                    $"{_vehicleApiOptions.BaseUrl}{string.Format(_vehicleApiOptions.GetModelsByMake, make)}";
                httpTest.ShouldHaveCalled(expectedUrl);

                foreach (var model in vehicleModels)
                {
                    expectedUrl =
                        $"{_vehicleApiOptions.BaseUrl}{string.Format(_vehicleApiOptions.GetYearsOfModelsByMake, make, model)}";
                    httpTest.ShouldHaveCalled(expectedUrl);
                }
            }

            // Assert
            vehicleApiModelsResponse.Make.Should().Be(make);
            vehicleApiModelsResponse.Models.Count.Should().Be(expectedVehicleApiModelsResponse.Models.Count);
            vehicleApiModelsResponse.Should().BeEquivalentTo(
                expectedVehicleApiModelsResponse,
                options => options.ComparingByMembers<VehicleSummaryResponse>());
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(vehicleApiModelsResponse));
        }


        [Fact]
        public async Task GetConsolidatedVehicleModelsAsync_WithNonExistingMake_ReturnsEmptyModels()
        {
            // Arrange
            var sut = new VehicleApiModelsService(_configuration);
            var make = "Bla";

            VehicleApiModelsResponse vehicleApiModelsResponse;

            // Act
            using (var httpTest = new HttpTest())
            {
                vehicleApiModelsResponse = await sut.GetConsolidatedVehicleModelsAsync(make);

                // Assert
                var expectedUrl =
                    $"{_vehicleApiOptions.BaseUrl}{string.Format(_vehicleApiOptions.GetModelsByMake, make)}";
                httpTest.ShouldHaveCalled(expectedUrl);
            }

            // Assert
            vehicleApiModelsResponse.Should().BeNull();
        }
        #endregion

        #region GetModelsByMakeAsync
        [Fact]
        public async Task GetModelsByMakeAsync_WithExistingMake_ReturnsExpectedModels()
        {
            // Arrange
            var sut = new VehicleApiModelsService(_configuration);
            var make = "Tesla";
            var expectedResponse = new List<string>
            {
                "Model Y",
                "Model S"
            };

            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(expectedResponse);

                // Act
                var response = await sut.GetModelsByMakeAsync(make);

                // Assert
                var expectedUrl =
                    $"{_vehicleApiOptions.BaseUrl}{string.Format(_vehicleApiOptions.GetModelsByMake, make)}";
                httpTest.ShouldHaveCalled(expectedUrl);
                response.Should().BeEquivalentTo(expectedResponse);
            }
        }

        [Fact]
        public async Task GetModelsByMakeAsync_WithNonExistingMake_ReturnsEmptyModels()
        {
            // Arrange
            var sut = new VehicleApiModelsService(_configuration);
            var make = "Tesla";

            using (var httpTest = new HttpTest())
            {
                // Act
                var response = await sut.GetModelsByMakeAsync(make);

                // Assert
                var expectedUrl =
                    $"{_vehicleApiOptions.BaseUrl}{string.Format(_vehicleApiOptions.GetModelsByMake, make)}";
                httpTest.ShouldHaveCalled(expectedUrl);
                response.Should().BeNull();
            }
        }
        #endregion

        #region GetYearsOfModelAsync
        [Fact]
        public async Task GetYearsOfModelAsync_WithExistingMakeAndModels_ReturnsExpectedYearsOfModel()
        {
            // Arrange
            var sut = new VehicleApiModelsService(_configuration);
            var make = "Tesla";
            var model = "Model S";
            var expectedYearsOfModel = new List<int> { 2019, 2020 };

            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(expectedYearsOfModel);

                // Act
                var response = await sut.GetYearsOfModelAsync(make, model);

                // Assert
                var expectedUrl =
                    $"{_vehicleApiOptions.BaseUrl}{string.Format(_vehicleApiOptions.GetYearsOfModelsByMake, make, model)}";
                httpTest.ShouldHaveCalled(expectedUrl);
                response.Should().BeEquivalentTo(expectedYearsOfModel);
            }
        }

        [Fact]
        public async Task GetYearsOfModelAsync_WithNonExistingMakeAndModels_ReturnsNull()
        {
            // Arrange
            var sut = new VehicleApiModelsService(_configuration);
            var make = "Jeepney";
            var model = "Jeep series 1";

            using (var httpTest = new HttpTest())
            {
                // Act
                var response = await sut.GetYearsOfModelAsync(make, model);

                // Assert
                var expectedUrl =
                    $"{_vehicleApiOptions.BaseUrl}{string.Format(_vehicleApiOptions.GetYearsOfModelsByMake, make, model)}";
                httpTest.ShouldHaveCalled(expectedUrl);
                response.Should().BeNull();
            }
        }
        #endregion
    }
}