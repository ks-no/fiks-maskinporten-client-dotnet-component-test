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
    public class MaskinportenClientComponentTests
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
                var token = await sut.GetAccessToken("ks");
            });
        }
        
        [Fact]
        public async Task GetsTokenIfCertificationIsValid()
        {
            if (!_fixture.CanRunTestWithProperCredentials())
            {
                return;
            }
            
            var sut = _fixture.CreateSut();
            var token = await sut.GetAccessToken("ks");
            token.Should().BeOfType<MaskinportenToken>();
        }
        
        [Fact]
        public async Task GetsTheSameTokenIfCallingTwiceInShortTime()
        {
            if (!_fixture.CanRunTestWithProperCredentials())
            {
                return;
            }
            
            var sut = _fixture.CreateSut();
            var token1 = await sut.GetAccessToken("ks");
            var token2 = await sut.GetAccessToken("ks");
            token1.Should().Be(token2);
        }
        
        [Fact]
        public async Task GetsNewTokenIfNumberOfSecondsLeftBeforeExpireIsLargerThanExp()
        {
            if (!_fixture.CanRunTestWithProperCredentials())
            {
                return;
            }
            
            var sut = _fixture.WithAHighNumberOfSecondsLeftBeforeExpire().CreateSut();
            var token1 = await sut.GetAccessToken("ks");
            
            var token2 = await sut.GetAccessToken("ks");
            token1.Should().NotBe(token2);
        }

        [Fact]
        public async Task TokenIsValidatedAtIdPorten()
        {
            if (!_fixture.CanRunTestWithProperCredentials())
            {
                return;
            }
            
            var sut = _fixture.CreateSut();
            var token = await sut.GetAccessToken("ks");
            var response = await _fixture.ValidateTokenWithIdPorten(token);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}