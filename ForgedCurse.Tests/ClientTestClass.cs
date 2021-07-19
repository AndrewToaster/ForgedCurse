using System.IO;
using ForgedCurse.Enumeration;
using ForgedCurse.Utility;
using ForgedCurse.Utility.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForgedCurse.Tests
{
	[TestClass]
    public class ClientTestClass
    {
        public const int AddonId = 231951;
        public const int AddonRelease = 3189063;

        public ForgeClient Client { get; }

        public ClientTestClass()
        {
            Client ??= new ForgeClient();
        }

        [TestMethod]
        public void MC_FmlVersions()
        {
            var result = Client.Minecraft.RetrieveModLoaderVersions().Result;
        }

        [TestMethod]
        public void MC_FmlVersion()
        {
            var versions = Client.Minecraft.RetrieveModLoaderVersions().Result;
            var version = Client.Minecraft.RetrieveModLoaderVersion(versions[0].Name).Result;
        }

        [TestMethod]
        public void MC_Versions()
        {
            var versions = Client.Minecraft.RetrieveGameVersions().Result;
        }

        [TestMethod]
        public void CF_SearchAddons()
		{
            var result = Client.Addons.SearchAddons(new AddonQueryBuilder().WithAmount(10).WithCategory(MinecraftCategory.Energy).WithGame(ForgeGames.Minecraft)).Result;
        }

        [TestMethod]
        public void CF_GetAddon()
        {
            var result = Client.Addons.RetriveAddon(AddonId).Result;
        }

        [TestMethod]
        public void CF_GetAddons()
        {
            var result = Client.Addons.RetriveAddons(AddonId).Result;
        }

        [TestMethod]
        public void FILE_GetHashAddon()
        {
            var result = Client.Files.SearchHashes(ForgeHash.ComputeHash(File.ReadAllBytes("./TestFiles/Jei.jar"))).Result;
        }

        [TestMethod]
        public void FILE_GetFiles()
        {
            var result = Client.Files.RetrieveReleases(AddonId).Result;
        }

        [TestMethod]
        public void FILE_GetFile()
        {
            var result = Client.Files.RetrieveRelease(AddonId, AddonRelease).Result;
        }

        [TestMethod]
        public void FILE_GetChangelog()
        {
            var result = Client.Files.RetrieveChangelog(AddonId, AddonRelease).Result;
        }

        [TestMethod]
        public void FILE_GetDl()
        {
            var result = Client.Files.RetrieveDownloadUrl(AddonId, AddonRelease).Result;
        }

        [TestMethod]
        public void CF_GetDl()
        {
            var result = Client.Addons.RetriveDescription(AddonId).Result;
        }
    }
}
