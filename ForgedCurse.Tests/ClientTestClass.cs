using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForgedCurse.Tests
{
    [TestClass]
    public class ClientTestClass
    {
        public ForgeClient Client { get; }

        public ClientTestClass()
        {
            Client = new ForgeClient();
        }

        [TestMethod]
        public void TestGetVersions()
        {
            Client.GetForgeVersions();
        }
    }
}
