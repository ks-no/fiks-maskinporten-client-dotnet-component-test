using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Ks.Fiks.Maskinporten.Client;
using Newtonsoft.Json;
using Xunit;

namespace KS.Fiks.Maskinporten.Client.ComponentTests
{
    public class MaskinportenClientComponentTests : IDisposable
    {
        private readonly MaskinportenClientFixture _fixture;

        public MaskinportenClientComponentTests()
        {
            _fixture = new MaskinportenClientFixture();
        }

        [Fact]
        public void CanBeCreated()
        {
            var sut = _fixture.CreateSut();
            sut.Should().NotBe(null);
        }

        [Fact]
        public async Task GetsUnexpectedResponseIfCertificateIsWrong()
        {
            await Assert.ThrowsAsync<UnexpectedResponseException>(async () =>
            {
                var sut = _fixture.WithUnauthorizedCertificate().CreateSut();
                var token = await sut.GetAccessToken("ks").ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task GetsTokenIfCertificationIsValid()
        {
            if (!MaskinportenClientFixture.CanRunTestWithProperCredentials())
            {
                return;
            }

            var sut = _fixture.CreateSut();
            var token = await sut.GetAccessToken("ks").ConfigureAwait(false);
            token.Should().BeOfType<MaskinportenToken>();
        }

        [Fact]
        public async Task GetsTheSameTokenIfCallingTwiceInShortTime()
        {
            if (!MaskinportenClientFixture.CanRunTestWithProperCredentials())
            {
                return;
            }

            var sut = _fixture.CreateSut();
            var token1 = await sut.GetAccessToken("ks").ConfigureAwait(false);
            var token2 = await sut.GetAccessToken("ks").ConfigureAwait(false);
            token1.Should().Be(token2);
        }

        [Fact]
        public async Task GetsNewTokenIfNumberOfSecondsLeftBeforeExpireIsLargerThanExp()
        {
            if (!MaskinportenClientFixture.CanRunTestWithProperCredentials())
            {
                return;
            }

            var sut = _fixture.WithAHighNumberOfSecondsLeftBeforeExpire().CreateSut();
            var token1 = await sut.GetAccessToken("ks").ConfigureAwait(false);

            var token2 = await sut.GetAccessToken("ks").ConfigureAwait(false);
            token1.Should().NotBe(token2);
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
                _fixture.Dispose();
            }
        }
    }
}