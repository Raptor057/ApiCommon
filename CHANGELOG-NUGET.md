# Changelog of the NuGet mirror

Changes to the packaging and publishing of `Raptor.Common.*` from this mirror. **What changed in the
library itself** is in [`CHANGELOG.md`](CHANGELOG.md), which comes 1:1 from Raptor-Dev-Services/Common.

Since 2.1.0 the package version is the Raptor-Dev-Services/Common tag it was copied from
(see `docs/adr-nuget/NUGET-0003`).

## [2.1.2] - 2026-09-24
Synced from Raptor-Dev-Services/Common `v2.1.2` (`83c390f`). Library changes: see `CHANGELOG.md`.

- Common now ships its own `CHANGELOG.md`, so this mirror's history moved here, to
  `CHANGELOG-NUGET.md`.
- Every package now includes its XML documentation file: IntelliSense shows the docs.
- CI and publish run on `ubuntu-24.04` with `actions/checkout@v7` and `actions/setup-dotnet@v6`.

## [2.1.1] - 2026-09-24
Synced from Raptor-Dev-Services/Common `v2.1.1` (`bd32469`). 2.1.0 was never published (the
upload was rejected): its content is included here.

- Security: SQL parameters logged by `DapperSqlDbConnectionBase` are now masked like the pipeline
  logs (before, an `INSERT` of a user wrote its password hash to the log). `DynamicParameters`
  included.
- Security: dictionaries and `ExpandoObject` are masked by key; before, each entry was logged as
  `{Key, Value}` with the value in clear text.
- `LICENSE` now comes from Common (same MIT text).
- Publishing uses NuGet Trusted Publishing (OIDC) instead of a stored API key (`NUGET-0004`).

## [2.1.0] - 2026-09-24 (not published)
Synced from Raptor-Dev-Services/Common `v2.1.0` (`3ed620f`). 2.0.0 was never published: its content
is included here.

- Adds the requirements specification (`docs/srs.md`) and the architecture decision records of the
  library (`docs/adr/`), plus the decisions specific to this NuGet mirror (`docs/adr-nuget/`).
- The package version now matches the Common tag; publish fails if the git tag and `version` differ.
- BREAKING: the code is now a 1:1 copy of Raptor-Dev-Services/Common.
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
