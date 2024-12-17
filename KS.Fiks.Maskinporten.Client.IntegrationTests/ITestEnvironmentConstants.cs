namespace KS.Fiks.Maskinporten.Client.IntegrationTests;

public interface ITestEnvironmentConstants
{
    string IdPortenCertFile { get; }

    string IdPortenCertPass { get; }

    string MaskinportenTokenEndpoint { get; }

    string MaskinportenAudience { get; }

    string MaskinportenIssuer { get; }

    int MaskinportenNumberOfSecondsLeftBeforeExpire { get; }
}