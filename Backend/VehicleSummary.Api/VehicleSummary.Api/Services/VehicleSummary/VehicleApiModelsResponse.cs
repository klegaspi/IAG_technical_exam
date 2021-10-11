using System.Collections.Generic;

namespace VehicleSummary.Api.Services.VehicleSummary
{
    public class VehicleApiModelsResponse
    {
        public string Make { get; set; }
        public Dictionary<string, List<int>> Models { get; set; } = new Dictionary<string, List<int>>();
    }
}