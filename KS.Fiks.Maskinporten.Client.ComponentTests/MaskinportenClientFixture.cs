using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Ks.Fiks.Maskinporten.Client;

namespace KS.Fiks.Maskinporten.Client.ComponentTests
{
    public class MaskinportenClientFixture : IDisposable
    {
        private const string IdPortenCertFile = ""; // Insert path to certificate to run locally

        private const string IdPortenCertPass = ""; // Insert credentials to run locally

        private const string MaskinportenTokenEndpoint = @"https://oidc-ver2.difi.no/idporten-oidc-provider/token";
        private const string MaskinportenAudience = @"https://oidc-ver2.difi.no/idporten-oidc-provider/";
        private const string MaskinportenIssuer = @"oidc_ks_test";
        private int _numberOfSecondsLeftBeforeExpire = 10;

        private MaskinportenClientConfiguration _configuration;
        private X509Certificate2 _certificate;

        public MaskinportenClientFixture()
        {
            if (CanRunTestWithProperCredentials())
            {
                _certificate = new X509Certificate2(IdPortenCertFile, IdPortenCertPass);
            }
            else
            {
                _certificate = new X509Certificate2(
                    "alice-virksomhetssertifikat.p12",
                    "PASSWORD");
            }
        }

        // This is needed until a proper way to inject credentials on the server is found
        public static bool CanRunTestWithProperCredentials()
        {
            return IdPortenCertFile.Length > 0;
        }

        public MaskinportenClient CreateSut()
        {
            _configuration = new MaskinportenClientConfiguration(
                MaskinportenAudience,
                MaskinportenTokenEndpoint,
                MaskinportenIssuer,
                _numberOfSecondsLeftBeforeExpire,
                _certificate);
            return new MaskinportenClient(_configuration);
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

        protected virtual void Dispose(bool disposed)
        {
            if (disposed)
            {
                _certificate.Dispose();
            }
        }
    }
}