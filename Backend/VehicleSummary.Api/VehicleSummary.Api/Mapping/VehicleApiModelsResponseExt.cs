using System.Linq;
using VehicleSummary.Api.Services.VehicleSummary;

namespace VehicleSummary.Api.Mapping
{
    public static class VehicleApiModelsResponseExt
    {
        public static VehicleSummaryResponse ToVehicleSummaryResponse(this VehicleApiModelsResponse vehicleApiModelsResponse)
        {
            if (vehicleApiModelsResponse?.Models is null)
            {
                return null;
            }
            var vehicleSummaryResponse = new VehicleSummaryResponse
            {
                Make = vehicleApiModelsResponse.Make
            };
            foreach (var model in vehicleApiModelsResponse.Models)
            {
                var vehicleSummaryModels = new VehicleSummaryModels
                {
                    Name = model.Key,
                    YearsAvailable = model.Value.Count()
                };
                vehicleSummaryResponse.Models.Add(vehicleSummaryModels);
            }
            return vehicleSummaryResponse;
        }
    }
}
