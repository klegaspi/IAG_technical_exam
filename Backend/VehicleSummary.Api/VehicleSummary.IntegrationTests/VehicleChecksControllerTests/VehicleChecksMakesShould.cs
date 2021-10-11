using FluentAssertions;
using Flurl.Http.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VehicleSummary.Api;
using VehicleSummary.Api.Services.VehicleSummary;
using Xunit;

namespace VehicleSummary.IntegrationTests.VehicleChecksControllerTests
{
    public class VehicleChecksMakesShould : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public VehicleChecksMakesShould(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.Server.PreserveExecutionContext = true;
        }

        [Fact]
        public async Task GetMakesAsync_WithExistingVehicleMakeAndModels_ReturnsExpectedVehicleSummaryResponse()
        {
            // Arrange
            var client = _factory.CreateClient();
            var make = "Tesla";
            var url = $"/vehicle-checks/makes/{make}";
            var vehicleApiModelsResponse = new VehicleApiModelsResponse
            {
                Make = make,
                Models = new Dictionary<string, List<int>>
                {
                    { "Model 3", new List<int> { 2007, 2008, 2009 } },
                    { "Model S", new List<int> { 2010, 2011 } }
                }
            };
            var expectedVehicleSummaryResponse = new VehicleSummaryResponse
            {
                Make = make,
                Models = new List<VehicleSummaryModels>
                {
                    new VehicleSummaryModels { Name = "Model 3", YearsAvailable=3 },
                    new VehicleSummaryModels { Name = "Model S", YearsAvailable=2 }
                }
            };

            using (var httpTest = new HttpTest())
            {
                var vehicleModels = vehicleApiModelsResponse.Models.Keys;
                httpTest.RespondWithJson(vehicleModels);
                foreach (var model in vehicleApiModelsResponse.Models)
                {
                    var yearOfModel = model.Value.ToArray();
                    httpTest.RespondWithJson(yearOfModel);
                }

                // Act
                var response = await client.GetAsync(url);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var result = response.Content.ReadAsStringAsync().Result;
                JsonConvert.DeserializeObject<VehicleSummaryResponse>(result).Should()
                    .BeEquivalentTo(expectedVehicleSummaryResponse);
            }
        }


        [Fact]
        public async Task GetMakesAsync_WithNonExistingVehicleMake_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            var make = "Bla";
            var url = $"/vehicle-checks/makes/{make}";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetMakesAsync_WithEmptyMakeParameter_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            var make = " ";
            var url = $"/vehicle-checks/makes/{make}/";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}