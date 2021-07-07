using System;

namespace ForgedCurse.Sections
{
    public class ForgeVersion
    {
        // Error
        Yo, dud, finish this class
        // Error

        public int id { get; set; }
        public int gameVersionId { get; set; }
        public int minecraftGameVersionId { get; set; }
        public string forgeVersion { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public string downloadUrl { get; set; }
        public string filename { get; set; }
        public int installMethod { get; set; }
        public bool latest { get; set; }
        public bool recommended { get; set; }
        public bool approved { get; set; }
        public DateTime dateModified { get; set; }
        public string mavenVersionString { get; set; }
        public string versionJson { get; set; }
        public string librariesInstallLocation { get; set; }
        public string minecraftVersion { get; set; }
        public object additionalFilesJson { get; set; }
        public int modLoaderGameVersionId { get; set; }
        public int modLoaderGameVersionTypeId { get; set; }
        public int modLoaderGameVersionStatus { get; set; }
        public int modLoaderGameVersionTypeStatus { get; set; }
        public int mcGameVersionId { get; set; }
        public int mcGameVersionTypeId { get; set; }
        public int mcGameVersionStatus { get; set; }
        public int mcGameVersionTypeStatus { get; set; }
        public object installProfileJson { get; set; }
    }
}