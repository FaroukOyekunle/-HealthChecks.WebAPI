namespace HealthChecks.WebApi.Models
{
    // Better Healthcheck Reponse.
    // Let's add a bunch of response classes to generate a more readable and meaningful response.
    // Response Class for Overall Health
    // We create another class that hold a list of IndividualHealthCheckResponses.
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<IndividualHealthCheckResponse> HealthChecks { get; set; }
        public TimeSpan HealthCheckDuration { get; set; }
    }
}
