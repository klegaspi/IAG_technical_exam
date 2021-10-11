using Flurl.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleSummary.Api.Settings;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    public interface IVehicleApiModelsService
    {
        Task<List<string>> GetModelsByMakeAsync(string make);
        Task<List<int>> GetYearsOfModelAsync(string make, string model);
        Task<VehicleApiModelsResponse> GetConsolidatedVehicleModelsAsync(string make);
    }
    
    public class VehicleApiModelsService : IVehicleApiModelsService
    {
        private readonly VehicleApiOptions _vehicleApiOptions;

        public VehicleApiModelsService(IConfiguration configuration)
        {
            _vehicleApiOptions = configuration.GetSection("VehicleApiSettings").Get<VehicleApiOptions>();
        }

        /// <summary>
        /// Gets models by make
        /// </summary>
        /// <param name="make"></param>
        /// <returns>a list of models</returns>
        public async Task<List<string>> GetModelsByMakeAsync(string make)
        {
            if (string.IsNullOrWhiteSpace(make))
            {
                throw new ArgumentNullException(nameof(make));
            }

            var url = $"{_vehicleApiOptions.BaseUrl}{string.Format(_vehicleApiOptions.GetModelsByMake, make)}";

            var response = await url
                .WithHeader("Ocp-Apim-Subscription-Key", _vehicleApiOptions.ApiKey)
                .GetJsonAsync<List<string>>();

            return response;
        }

        /// <summary>
        /// Gets years of model
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<int>> GetYearsOfModelAsync(string make, string model)
        {
            if (string.IsNullOrWhiteSpace(make))
            {
                throw new ArgumentNullException(nameof(make));
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentNullException(nameof(model));
            }

            var url = $"{_vehicleApiOptions.BaseUrl}{string.Format(_vehicleApiOptions.GetYearsOfModelsByMake, make, model)}";

            var response = await url
                .WithHeader("Ocp-Apim-Subscription-Key", _vehicleApiOptions.ApiKey)
                .GetJsonAsync<List<int>>();

            return response;
        }

        /// <summary>
        /// Gets models and years by make
        /// </summary>
        /// <param name="make"></param>
        /// <returns></returns>
        public async Task<VehicleApiModelsResponse> GetConsolidatedVehicleModelsAsync(string make)
        {
            if (string.IsNullOrWhiteSpace(make))
            {
                throw new ArgumentNullException(nameof(make));
            }

            var vehicleApiModelResponse = new VehicleApiModelsResponse();
            var models = await GetModelsByMakeAsync(make);
            
            if (models is null)
            {
                return null;
            }

            vehicleApiModelResponse.Make = make;
            foreach (var model in models)
            {
                var yearsOfModel = await GetYearsOfModelAsync(make, model);
                vehicleApiModelResponse.Models.Add(model, yearsOfModel);
            }

            return vehicleApiModelResponse;
        }
    }
}