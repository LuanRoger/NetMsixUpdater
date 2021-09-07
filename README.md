# NetMsixUpdater
### NetMsixUpdater is a library that allows complete and built-in updates to .msix programs.

![version](https://img.shields.io/nuget/dt/NetMsixUpdater)
![last](https://img.shields.io/nuget/v/NetMsixUpdater)

## Dependencies
- .NET 5
- YamlDotNet (>= 11.2.1)

## Installation
### PM
```
Install-Package NetMsixUpdater -Version 0.0.1
```
### .NET CLI
```
dotnet add package NetMsixUpdater --version 0.0.1
```
See also in [NuGet Gallery](https://www.nuget.org/packages/NetMsixUpdater)

## Example
```yaml
version: 0.6.9.0
url: https://github.com/LuanRoger/ProjectBook/releases/download/v0.6.9-beta/ProjectBook.Pakage_0.6.9.0_AnyCPU.msix
extension: .msix
changelog: https://github.com/LuanRoger/ProjectBook/releases/tag/v0.6.9-beta
mandatory: false
```
> Yaml info file

```chsarp
MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
msixUpdater.Build();

UpdateExtension.OnDownlaodComplete += (_) =>
{
    Debug.WriteLine("Done.");
};

msixUpdater.DownlaodUpdate(Consts.MSIX_OUTPUT);
```
> C# code to only download update

## Documentation
Documentation is available on the [Wiki](https://github.com/LuanRoger/NetMsixUpdater/wiki)
