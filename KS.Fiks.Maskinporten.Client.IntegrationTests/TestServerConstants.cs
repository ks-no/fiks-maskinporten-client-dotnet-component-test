namespace KS.Fiks.Maskinporten.Client.IntegrationTests
{
    public class TestServerConstants : ITestEnvironmentConstants
    {
        public string Scheme => "https";

        public int Port => 443;

        public string IdPortenCertFile => "secret-auth.p12"; // Insert path to certificate to run locally

        public string IdPortenCertPass => "${MASKINPORTEN_CERT_PWD}"; // Swapped with password in jenkins pipeline

        public string MaskinportenTokenEndpoint => @"https://oidc-ver2.difi.no/idporten-oidc-provider/token";

        public string MaskinportenAudience => @"https://oidc-ver2.difi.no/idporten-oidc-provider/";

        public string MaskinportenIssuer => @"oidc_ks_test";

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