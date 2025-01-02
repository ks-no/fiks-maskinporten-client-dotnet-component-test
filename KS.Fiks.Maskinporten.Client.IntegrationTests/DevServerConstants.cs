using System;
using System.Configuration;

namespace KS.Fiks.Maskinporten.Client.IntegrationTests;

public class DevServerConstants : ITestEnvironmentConstants
{
    public string IdPortenCertFile => Environment.GetEnvironmentVariable("MASKINPORTEN_CERT")
                                      ?? throw new ConfigurationErrorsException("MASKINPORTEN_CERT_PWD environment variable not set");

    public string IdPortenCertPass => Environment.GetEnvironmentVariable("MASKINPORTEN_CERT_PWD")
                                      ?? throw new ConfigurationErrorsException("MASKINPORTEN_CERT_PWD environment variable not set");

    public string MaskinportenTokenEndpoint => @"https://test.maskinporten.no/token";

    public string MaskinportenAudience => @"https://test.maskinporten.no/";

    public string MaskinportenIssuer => @"77c0a0ba-d20d-424c-b5dd-f1c63da07fc4";

    public int MaskinportenNumberOfSecondsLeftBeforeExpire => 10;
}