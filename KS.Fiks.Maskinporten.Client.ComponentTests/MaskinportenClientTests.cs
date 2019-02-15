using FluentAssertions;
using Xunit;

namespace KS.Fiks.Maskinporten.Client.ComponentTests
{
    public class MaskinportenClientComponentTests
    {
        private MaskinportenClientFixture _fixture;


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
    }
}