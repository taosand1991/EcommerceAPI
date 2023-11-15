
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Twilio;
using Twilio.Rest.Verify.V2;
using Twilio.Rest.Verify.V2.Service;

namespace EcommerceAPI.Data
{
    public class TwilioAPI
    {

        private readonly string AccountSid;

        private readonly string AccountToken;

        private string SidNumber;

        public string Status;

        public string? PhoneNumber { get; set; }
        public TwilioAPI()
        {
            this.AccountSid = Environment.GetEnvironmentVariable("TwilioSid")!;
            this.AccountToken = Environment.GetEnvironmentVariable("TwilioToken")!;
            this.SidNumber = Environment.GetEnvironmentVariable("SidNumber")!;
            this.Status = Environment.GetEnvironmentVariable("Status")!;
        }

        public void CreateService()
        {
            Console.WriteLine($"AccountSid: {AccountSid}");
            Console.WriteLine($"AccountToken: {AccountToken}");
            TwilioClient.Init(AccountSid, AccountToken);

            var service = ServiceResource.Create(friendlyName: "A new Service");

            this.SidNumber = service.Sid;
        }

        public VerificationResource SendVerificationToken(string phoneNumber)
        {
            TwilioClient.Init(AccountSid, AccountToken);

            Console.WriteLine($"PhoneNumber: {phoneNumber}");

            Console.WriteLine($"serviceID1: {this.SidNumber}");

            var verification = VerificationResource.Create(
                to: phoneNumber,
                channel: "sms",
                pathServiceSid: this.SidNumber
            );


            Environment.SetEnvironmentVariable("SidNumber", verification.ServiceSid);
            Environment.SetEnvironmentVariable("Status", verification.Status);

            return verification;
            
        }

        public void VerifyToken(string phoneNumber, string code)
        {
            TwilioClient.Init(AccountSid, AccountToken);

            Console.WriteLine($"code: {code}");

            Console.WriteLine($"serviceID: {this.SidNumber}");

            var verification = VerificationCheckResource.Create(
                to: phoneNumber,
                code: code,
                pathServiceSid: this.SidNumber
            );

            Environment.SetEnvironmentVariable("Status", verification.Status);
        }
    }
}
