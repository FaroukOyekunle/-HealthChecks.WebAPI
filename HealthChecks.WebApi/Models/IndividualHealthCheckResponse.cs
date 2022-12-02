namespace HealthChecks.WebApi.Models
{
    // Better Healthcheck Reponse.
    // Let's add a bunch of response classes to generate a more readable and meaningful response.
    // Response Class for Component-wise Health
    public class IndividualHealthCheckResponse
    {
        public string Status { get; set; }
        public string Components { get; set; }
        public string Description { get; set; }
    }
}
