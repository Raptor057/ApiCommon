# Changelog

All notable changes to this project will be documented in this file.
This project follows Semantic Versioning.

## [2.0.0] - 2026-09-24
- BREAKING: the code is now a 1:1 copy of Raptor-Dev-Services/Common (upstream `aedf830`).
- BREAKING: targets net10.0 (was net8.0). MediatR and Newtonsoft.Json are gone; the library
  ships its own mediator (`Common.Messaging`).
- BREAKING: split into five packages plus a facade: `Raptor.Common.Contracts`,
  `Raptor.Common.Messaging`, `Raptor.Common.MultiTenancy`, `Raptor.Common.Infra`,
  `Raptor.Common.Web`. `Raptor.Common` depends on all five, so existing references keep resolving.
- Adds Serilog + Seq logging, OpenTelemetry, health checks (PostgreSQL/Redis), HTTP resilience,
  Dapper/Npgsql data access, multi-tenancy and web helpers.
- Packaging lives in `Directory.Build.props` and the version in the root `version` file.
- CI and publish now run the stable-dependencies gate (`scripts/check-prerelease-deps.py`).

## [0.0.10] - 2025-12-24
- Fixed NuGet packing by correcting README package path.

## [0.0.9] - 2025-12-24
- Fixed publish pipeline by pinning .NET SDK and forcing valid repository URLs.

## [0.0.8] - 2025-12-24
- Fixed publish pipeline by pinning .NET SDK and normalizing repository URLs.

## [0.0.7] - 2025-12-24
- Added LICENSE, CHANGELOG, CI workflow, and SourceLink metadata.
- Enabled XML documentation generation and repository metadata.

## [0.0.6] - 2025-12-24
- Flattened library paths into `Common/` and aligned solution structure.
- Added xUnit test suite and CI-ready structure.
- Updated documentation and release process.
- Version is now read from `Common/version` during build.

## [0.0.5] - 2025-12-24
- Previous internal release.
