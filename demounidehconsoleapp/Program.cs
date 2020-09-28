using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Threading.Tasks;

namespace demounideconsoleapp
{
    class Program
    {
        //pvgnacbemhsj67k34t2bbkxpfiszmmpfcobgcaj3dqp6c5rdrn3a
        static void Main(string[] args)
        {
            if (args.Length == 0)//(args.Length == 3)
            {

                Uri orgUrl = new Uri("https://dev.azure.com/demounideh"); //new Uri(args[0]);         // Organization URL, for example: https://dev.azure.com/fabrikam               
                String personalAccessToken = "pvgnacbemhsj67k34t2bbkxpfiszmmpfcobgcaj3dqp6c5rdrn3a";  //args[1];  // See https://docs.microsoft.com/azure/devops/integrate/get-started/authentication/pats
                int workItemId = 2; //int.Parse(args[2]);   // ID of a work item, for example: 12

                // Create a connection
                VssConnection connection = new VssConnection(orgUrl, new VssBasicCredential(string.Empty, personalAccessToken));

                // Show details a work item
                ShowWorkItemDetails(connection, workItemId).Wait();
            }
            else
            {
                Console.WriteLine("Usage: ConsoleApp {orgUrl} {personalAccessToken} {workItemId}");
            }
        }

        static private async Task ShowWorkItemDetails(VssConnection connection, int workItemId)
        {
            // Get an instance of the work item tracking client
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                // Get the specified work item
                WorkItem workitem = await witClient.GetWorkItemAsync(workItemId);

                // Output the work item's field values
                foreach (var field in workitem.Fields)
                {
                    Console.WriteLine("  {0}: {1}", field.Key, field.Value);
                }
            }
            catch (AggregateException aex)
            {
                VssServiceException vssex = aex.InnerException as VssServiceException;
                if (vssex != null)
                {
                    Console.WriteLine(vssex.Message);
                }
            }
        }
    }
}
