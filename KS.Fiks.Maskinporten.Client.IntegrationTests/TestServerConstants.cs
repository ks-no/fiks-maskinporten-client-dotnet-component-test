using System;
using System.Configuration;

namespace KS.Fiks.Maskinporten.Client.IntegrationTests
{
    public class TestServerConstants : ITestEnvironmentConstants
    {
        public string Scheme => "https";

        public int Port => 443;

        public string IdPortenCertFile => "secret-auth.p12";

        public string IdPortenCertPass => Environment.GetEnvironmentVariable("MASKINPORTEN_CERT_PWD")
                                          ?? throw new ConfigurationErrorsException("MASKINPORTEN_CERT_PWD environment variable not set");

        public string MaskinportenTokenEndpoint => @"https://test.maskinporten.no/token";

        public string MaskinportenAudience => @"https://test.maskinporten.no/";

        public string MaskinportenIssuer => @"77c0a0ba-d20d-424c-b5dd-f1c63da07fc4";

        public int MaskinportenNumberOfSecondsLeftBeforeExpire => 10;

        public string IntegrasjonId => "3be28b0e-1fa5-4c6f-a947-cf5e0b2191b1";

        public string DefaultAccount => "eec2655a-6bbc-40b3-932b-86904521171e";

        public string IntegrasjonPassword => "nFXo.ljLe)jcpjFJg02HOP9ECHSh@6jTMg_SO77V~N(i(zQ6v3";

        public string Host => "api.fiks.test.ks.no";

        public string Path => "/svarinn2/katalog/api/v1";

        public string AccountId => "eec2655a-6bbc-40b3-932b-86904521171e";

        public string OrganizationId => "81865fdb-3764-438a-8ec6-eb0d738834dc";

        public string AmqpHost => "api.fiks.test.ks.no";

        public int AmqpPort => 5672;
    }
}