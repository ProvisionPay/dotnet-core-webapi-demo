using ProvisionPay;

namespace dotnet.demo.webapi
{
    public class SoftPosDeeplinkDemoConfiguration : ISoftPosDeeplinkConfiguration
    {
        public EnvironmentType Environment => EnvironmentType.Test;

        public Credentials Credentials => new Credentials("Your Login Id", "Your Password");

        public SoftposDeeplinkSDKRegion Region => SoftposDeeplinkSDKRegion.Region1;
    }
}