# Agent Handoff Guide

This file is intended for future coding agents and maintainers making changes in this repository.

## What This Repository Is

- `GslCore` is the main compiler/core library for the Genotype Specification Language (GSL).
- Primary code is in `src/GslCore`.
- Tests are in `tests/GslCore.Tests`.
- Build orchestration is in `build/Program.fs` (FAKE targets compiled as a normal .NET executable project).

## Current Baseline (0.6.0)

- All projects target `net10.0`.
- SDK pinned via `global.json` (`10.0.100`, roll-forward `latestFeature`).
- Dependencies use Central Package Management (`Directory.Packages.props`).
- Paket has been removed from the active build/restore flow.

## Build and Test Commands

- Restore: `dotnet restore GslCore.sln`
- Build/test/package orchestration: `dotnet run --project build -- All`
- Direct tests: `dotnet test tests/GslCore.Tests/GslCore.Tests.fsproj -c Release`

## Release Mechanics

- Release notes are in `RELEASE_NOTES.md`; newest entry should be at the top.
- Build script reads release notes to determine package version.
- NuGet package target: `NuGet` (writes `.nupkg` to `bin/`).
- NuGet publish target: `PublishNuget`.

Environment variables used by publishing:

- `NUGET_KEY` (required)
- `NUGET_SOURCE` (optional, defaults to `https://api.nuget.org/v3/index.json`)

## Important Files

- `Directory.Packages.props`: central package versions.
- `src/GslCore/GslCore.fsproj`: library project, package metadata, FsLex/FsYacc config.
- `tests/GslCore.Tests/GslCore.Tests.fsproj`: test project.
- `build/build.fsproj`: build tool project dependencies.
- `build/Program.fs`: FAKE targets and release automation.

## Safety Notes for Future Changes

- Prefer keeping package versions centralized; do not add inline versions in project files.
- Keep `RELEASE_NOTES.md` and package metadata coherent for each release.
- If updating FAKE packages, update all FAKE package versions together unless a targeted hotfix is required.
- Validate with restore + build + tests before concluding migration work.
