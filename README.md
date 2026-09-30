# Genotype Specification Language (GSL) Core Library

Amyris/Demetrix domain specification language and compiler core for rapidly specifying genetic designs.

## Links

- Scientific paper: http://pubs.acs.org/doi/abs/10.1021/acssynbio.5b00194
- Historical Autodesk Genetic Constructor docs: https://geneticconstructor.readme.io/docs/genotype-specification-language
- Press release: http://investors.amyris.com/releasedetail.cfm?ReleaseID=992005

## Repository Structure

- `src/GslCore`: core library and compiler implementation.
- `tests/GslCore.Tests`: NUnit-based test suite.
- `build`: FAKE-based build orchestration executable.
- `docs`: design and language documentation.

## Prerequisites

- .NET SDK `10.0.100` or newer compatible `10.0.x` feature band.

## Build and Test

Windows:

```powershell
./build.cmd
```

Linux/macOS:

```bash
./build.sh
```

Equivalent raw commands:

```bash
dotnet restore GslCore.sln
dotnet run --project build -- All
```

Direct test run:

```bash
dotnet test tests/GslCore.Tests/GslCore.Tests.fsproj -c Release
```

## Packaging

The NuGet package is produced by the build target `NuGet`:

```bash
dotnet run --project build -- NuGet
```

Artifacts are written to `bin/`.

## Dependency Management

This repository uses SDK-style `PackageReference` with Central Package Management in `Directory.Packages.props`.

## Release Notes

See `RELEASE_NOTES.md` for version history.
