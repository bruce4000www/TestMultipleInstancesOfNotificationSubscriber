using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core;
using Task = System.Threading.Tasks.Task;

namespace NotificationSubscriber.Handlers
{
    [FhirHandlerClass(AcceptedType = nameof(Organization))]
    public class OrganizationFhirDataHandler(TestResultDbContext testResultDbContext)
    {
        [AsyncFhirHandler("OrganizationConsumer", HandlerCategory.PostInteraction, FhirInteractionType.Create, Publish = true)]
        public async Task OrganizationConsumer(IFhirContext context, Organization organization, CancellationToken cancellationToken)
        {
            await Task.Delay(Random.Shared.Next(500, 5000), cancellationToken); // Simulate some processing delay
            var code = organization.Meta.Tag.Where(x => x.System == "RunningNumber").FirstOrDefault()?.Code ?? "0";
            // Get the container name
            string containerName = Environment.MachineName;
            testResultDbContext.Add(new LogResult
            {
                MachineName = containerName,
                TagCode = code,
                CreationTime = DateTimeOffset.Now
            });
            await testResultDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
