# PDF Document Parser — PoC

A proof-of-concept REST API for parsing PDF documents using [pdf.co](https://pdf.co) service.

Built with ASP.NET Core, JWT Bearer authentication, and a clean multi-project architecture.

## Architecture

Solution is split into 3 projects:

- **Pdf.Api** — REST API with JWT Bearer auth, Swagger UI, PDF parsing endpoint
- **Pdf.Web** — MVC web app with signup/sign-in (Identity, in-memory DB)
- **Pdf.DAL** — Shared `ApplicationDbContext` between API and Web

## API Endpoints

### Token
Bearer token auth. For PoC, a simplified token can be retrieved by providing username only.

### ParsePdfFromUrl
Requires `Authorization: Bearer TOKEN` header. Sends PDF URL to pdf.co for parsing and returns structured result.

## Tech Stack

- ASP.NET Core
- Entity Framework Core (in-memory for PoC)
- JWT Bearer Authentication
- Swagger / OpenAPI
- pdf.co API integration

## Setup

1. Add your pdf.co API key to `appsettings.json` → `PdfConfig:DefaultApiKey`
2. Add your JWT secret to `appsettings.json` → `JwtConfig:secret`
3. Run the API project: `dotnet run --project src/Bytescout.Pdf/Bytescout.Pdf.Api`
