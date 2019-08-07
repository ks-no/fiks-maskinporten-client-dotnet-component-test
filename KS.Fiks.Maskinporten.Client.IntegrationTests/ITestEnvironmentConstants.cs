namespace KS.Fiks.Maskinporten.Client.IntegrationTests
{
    public interface ITestEnvironmentConstants
    {
        string Scheme { get; }

        int Port { get; }

        string IdPortenCertFile { get; }

        string IdPortenCertPass { get; }

        string MaskinportenTokenEndpoint { get; }

        string MaskinportenAudience { get; }

        string MaskinportenIssuer { get; }

        int MaskinportenNumberOfSecondsLeftBeforeExpire { get; }

        string IntegrasjonId { get; }

        string IntegrasjonPassword { get; }

        string Host { get; }

        string DefaultAccount { get; }

        string Path { get; }

        string AccountId { get; }

        string OrganizationId { get; }

        string AmqpHost { get; }

        int AmqpPort { get; }
    }
}