using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SendGrid.Helpers.Mail;

namespace Siliconvalve.FunctionDemo01
{
    public static class TimerJobSample
    {
        [FunctionName("TimerJobSample")]
        public static void Run([TimerTrigger("*/10 * * * * *")]TimerInfo myTimer, [SendGrid(From = "%NotificationsSender%")] out SendGridMessage message, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTimeOffset.UtcNow}");

            message = new SendGridMessage();

            try
            {
                message.AddContent("text/plain", "Read the blog at https://blog.siliconvalve.com/");
//                message.AddTo(GetEnvironmentVariable("TimerRecipient"));
                message.AddContent("text/plain", "TimerRecipient environment variable=="+GetEnvironmentVariable("TimerRecipient"));
                message.AddTo("dwhanger@hotmail.com");
                message.Subject = "The blog to read for Azure info.";
            }
            catch (Exception ex)
            {
                log.Error("TimerJobSample unhandled Exception", ex);
            }
        }

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
