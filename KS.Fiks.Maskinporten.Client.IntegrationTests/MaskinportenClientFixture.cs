using System;
using System.Security.Cryptography.X509Certificates;
using Ks.Fiks.Maskinporten.Client;

namespace KS.Fiks.Maskinporten.Client.IntegrationTests;

public sealed class MaskinportenClientFixture : IDisposable
{
    public string DefaultScope => "ks:fiks";

    private readonly ITestEnvironmentConstants _testEnvironmentConstants;
    private X509Certificate2 _certificate;
    private int _numberOfSecondsLeftBeforeExpire;

    public MaskinportenClientFixture()
    {
        _testEnvironmentConstants = new DevServerConstants();
        _certificate = new X509Certificate2(_testEnvironmentConstants.IdPortenCertFile, _testEnvironmentConstants.IdPortenCertPass);
        _numberOfSecondsLeftBeforeExpire = _testEnvironmentConstants.MaskinportenNumberOfSecondsLeftBeforeExpire;
    }

    public MaskinportenClient CreateSut()
    {
        var maskinportenConfiguration = new MaskinportenClientConfiguration(
            _testEnvironmentConstants.MaskinportenAudience,
            _testEnvironmentConstants.MaskinportenTokenEndpoint,
            _testEnvironmentConstants.MaskinportenIssuer,
            _numberOfSecondsLeftBeforeExpire,
            _certificate);
        return new MaskinportenClient(maskinportenConfiguration);
    }

    public MaskinportenClientFixture WithUnauthorizedCertificate()
    {
        _certificate = new X509Certificate2(
            "alice-virksomhetssertifikat.p12",
            "PASSWORD");
        return this;
    }

    public MaskinportenClientFixture WithAHighNumberOfSecondsLeftBeforeExpire()
    {
        _numberOfSecondsLeftBeforeExpire = 1000000;
        return this;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposed)
    {
        if (disposed)
        {
            _certificate.Dispose();
        }
    }
}