![Icon](Icon.png)
# ForgedCurse
A C# wrapper around the CurseForge API

# Usage
The heart of this package is the `ForgeClient` class. It communicates with the CurseForge API and does it's stuff.

```csharp
ForgeClient client = new ForgeClient();
```
It is as easy as that, now you have access to the API. 

```csharp
// Retrieves information about the mod with id 238222 (Just Enough Items)
Addon addon = client.Addons.RetrieveAddon(238222);

// Prints the first author of the mod
Console.WriteLine(addon.Authors[0].Name);
```

# Fingerprinting / Hashing
CurseForge uses MurmurrHash2 as the hashing method for addons. This API exposes
the `ForgeHash` class for computing hashes
```csharp
// Reads the contents of a mod file
byte[] data = File.ReadAllBytes("C:/jei.jar");

// Compute the fingerprint using the Murmurrhash2
uint hash = ForgeHash.ComputeHash(data);

// Retrieve the search result based on the provided hash
HashSearchResult result = client.Files.SearchHashes(hash);

// Get the file from the hash (if found)
Release modRelease = result.Hits[0].File;
```
