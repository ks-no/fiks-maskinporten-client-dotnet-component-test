using Ks.Fiks.Maskinporten.Client;

namespace KS.Fiks.Maskinporten.Client.ComponentTests
{
    public class MaskinportenClientFixture
    {
        private MaskinportenClientProperties _properties;

        public MaskinportenClientFixture()
        {
            _properties = new MaskinportenClientProperties("test", "http://fikst.test.ks.no", "test", 1);
        }
        
        public MaskinportenClient CreateSut()
        {
            return new MaskinportenClient(_properties);
        }
    }
}