namespace KS.Fiks.Maskinporten.Client.IntegrationTests
{
    public class DevServerConstants : ITestEnvironmentConstants
    {
        public string Scheme => "https";

        public int Port => 443;

        public string IdPortenCertFile => "secret-auth.p12"; // Insert path to certificate to run locally

        public string IdPortenCertPass => "${MASKINPORTEN_CERT_PWD}"; // Swapped with password in jenkins pipeline

        public string MaskinportenTokenEndpoint => @"https://oidc-ver2.difi.no/idporten-oidc-provider/token";

        public string MaskinportenAudience => @"https://oidc-ver2.difi.no/idporten-oidc-provider/";

        public string MaskinportenIssuer => @"oidc_ks_test";

        public int MaskinportenNumberOfSecondsLeftBeforeExpire => 10;

        public string IntegrasjonId => "1d9a81ed-b1d4-46c2-bc35-6c331298d38a";

        public string IntegrasjonPassword => "A6_.z{gAt0ldfzuo5{P%0L2#%n#-jrtIb%&qzeW^gw2NOUH}F0";

        public string Host => "api.fiks.dev.ks.no";

        public string DefaultAccount => "a504eb17-212f-4295-9b94-53167ea8f143";

        public string Path => "/fiks-io/katalog/api/v1";

        public string AccountId => "a504eb17-212f-4295-9b94-53167ea8f143";

        public string OrganizationId => "3ba20d52-490d-497a-af6a-5d10577cd2fd";

        public string AmqpHost => "ksjenkins.usrv.ubergenkom.no";

        public int AmqpPort => 5671;
    }
}