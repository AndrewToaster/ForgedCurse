![Icon](Icon.png)
# ForgedCurse
A C# wrapper around the CurseForge API

# Usage
The heart of this package is the `ForgeClient` class. It communicated with the CurseForge API and does it's stuff.

```csharp
ForgeClient client = new ForgeClient();
```
It is as easy as that, now you have access to the API. 

```csharp
// Retrieves information about the mod with id 238222 (Just Enough Items)
CurseJSON.AddonInfo addon = client.GetAddon(238222);

// Prints the first author of the mod
Console.WriteLine(addon.authors[0]);
```

# Fingerprinting
I don't have the education to properly explain what finprinting is, but it is a way to provide a hash for a addon package. It is possible to use these hashes to retrieve information
about the addons and such. For anyone wondering if you can compute a fingerprint? Yes, quite easily.
```csharp
Span<byte> data = File.ReadBytes("C:/jei.jar");
long fingerprint = Utilities.ComputeFingerprint(data);

CurseJSON.PackageFingerprint fpData = client.GetPackageFingerprint(fingerprint);
CurseJSON.AddonFile file = fpData.exactMachtes[0].file;
```