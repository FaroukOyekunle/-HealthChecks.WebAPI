using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks.WebApi.HealthChecks
{
    // It is simple to incorporate databases and services into the healthchecks.
    // But what if we require a completely customized health with your logics?
    // That's exactly what i did here.
    // creating  a class CustomHealthCheck.cs

    // Line #11 – I implement from the interface, IHealthCheck
    // Line #14 – By default, we will have to implement the CheckHealthAsync method.
    // Line #20 – Here we mimic a random exception.
    // Line #25 - I return the error message and the health report's status as unhealthy.
    // PS: You also have the option of setting the health to degrading.
    public class CustomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                throw new Exception("Random Error Caught!");
            }
            catch (Exception ex)
            {

                return Task.FromResult(HealthCheckResult.Unhealthy(ex.Message));
            }
        }
    }
}
