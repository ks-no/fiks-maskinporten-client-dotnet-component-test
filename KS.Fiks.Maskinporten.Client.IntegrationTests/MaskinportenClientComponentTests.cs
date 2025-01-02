using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using FluentAssertions;
using Ks.Fiks.Maskinporten.Client;
using KS.Fiks.Maskinporten.Client.Builder;
using Xunit;

namespace KS.Fiks.Maskinporten.Client.IntegrationTests;

public sealed class MaskinportenClientComponentTests : IDisposable
{
    private readonly MaskinportenClientFixture _fixture = new();

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
            var token = await sut.GetAccessToken(_fixture.DefaultScope).ConfigureAwait(false);
        }).ConfigureAwait(false);
    }

    [Fact]
    public async Task GetsTokenIfCertificationIsValid()
    {
        var sut = _fixture.CreateSut();
        var token = await sut.GetAccessToken(_fixture.DefaultScope).ConfigureAwait(false);
        token.Should().BeOfType<MaskinportenToken>();
    }

    [Fact]
    public async Task GetsTheSameTokenIfCallingTwiceInShortTime()
    {
        var sut = _fixture.CreateSut();
        var token1 = await sut.GetAccessToken(_fixture.DefaultScope).ConfigureAwait(false);
        var token2 = await sut.GetAccessToken(_fixture.DefaultScope).ConfigureAwait(false);
        token1.Should().Be(token2);
    }

    [Fact]
    public async Task GetsNewTokenIfNumberOfSecondsLeftBeforeExpireIsLargerThanExp()
    {
        var sut = _fixture.WithAHighNumberOfSecondsLeftBeforeExpire().CreateSut();
        var token1 = await sut.GetAccessToken(_fixture.DefaultScope).ConfigureAwait(false);

        var token2 = await sut.GetAccessToken(_fixture.DefaultScope).ConfigureAwait(false);
        token1.Should().NotBe(token2);
    }

    [Fact]
    public async Task GetsTokenWithPidWhenRequested()
    {
        var sut = _fixture.CreateSut();
        var tokenRequest = new TokenRequestBuilder()
            .WithScopes("ks:fiks")
            .WithPid("12345678901")
            .Build();

        var maskinportenToken = await sut.GetAccessToken(tokenRequest).ConfigureAwait(false);

        maskinportenToken.Should().BeOfType<MaskinportenToken>();

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(maskinportenToken.Token);
        var pidFromToken = jwtToken.Payload["pid"] as string;

        pidFromToken.Should().NotBeNull();
        pidFromToken.Should().Be("12345678901");
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
            _fixture.Dispose();
        }
    }
}