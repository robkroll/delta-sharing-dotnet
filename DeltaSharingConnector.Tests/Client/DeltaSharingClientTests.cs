using DeltaSharingConnector.Client;
using NUnit.Framework;

namespace DeltaSharingConnector.Tests.Client
{
    [TestFixture]
    public class DeltaSharingClientTests
    {
        [Test]
        public void ListSharesTest()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Client\open-datasets.share");
            var profile =  DeltaSharingProfile.FromFile(path);
            var client = new DeltaSharingRestClient(profile);

            // var shares = client.ListShares();
            var share = client.GetShare("delta_sharing");

            Assert.IsNotNull(share);
        }
    }
}
