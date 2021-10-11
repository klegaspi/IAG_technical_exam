namespace VehicleSummary.Api.Settings
{
    public class VehicleApiOptions
    {
        public const string VehicleApiSettings = "VehicleApiSettings";
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string GetModelsByMake { get; set; }
        public string GetYearsOfModelsByMake { get; set; }
    }
}