# AGENTS.md

## Project overview

ASP.NET Core (net10.0) backend Web API following a 4-layer architecture
(`Core`, `Application`, `Infrastructure`, `WebApi`) under `src/`. See the
project conventions in the user/project rules. The current code is a minimal
"HelloWorld" greeting API used to validate the dev environment:

- `Core/Entities/Greeting.cs` — domain entity
- `Application` — `Result<T>`, DTOs, service interfaces/impls
- `Infrastructure/Persistence/Repositories/GreetingRepository.cs` — in-memory repository (no database required)
- `WebApi` — controllers, `ApiResponse<T>`, `Program.cs`

## Cursor Cloud specific instructions

- The .NET 10 SDK is preinstalled in the VM image and `dotnet` is on `PATH`
  (symlinked at `/usr/local/bin/dotnet`). The startup update script only runs
  `dotnet restore`; it does not reinstall the SDK.
- The solution uses the new `.slnx` XML format (`HelloWorld.slnx`), not `.sln`.
  Always pass `HelloWorld.slnx` to commands that take a solution path.
- Common commands (run from repo root):
  - Build: `dotnet build HelloWorld.slnx`
  - Lint/format check: `dotnet format HelloWorld.slnx --verify-no-changes`
  - Run (Development): `dotnet run --project src/WebApi/WebApi.csproj`
- The dev server listens on `http://localhost:5169` (HTTP only, no HTTPS),
  configured via `src/WebApi/Properties/launchSettings.json`. It defaults to the
  `Development` environment, which exposes the OpenAPI doc at `/openapi/v1.json`.
- Persistence is in-memory, so data resets on each restart and no database/
  connection string is needed to run or test the API.
- There is no automated test project yet; verify changes by building and
  exercising endpoints (e.g. `GET /health`, `GET/POST /api/greeting`).
