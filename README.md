# clean-architecture-dotnet-10

A sample e-commerce microservice solution built with .NET 10 using Clean Architecture principles.

## Overview

This repository contains a multi-service application with the following components:

- `Catalog` service
- `Basket` service
- `Discount` service
- `catalogdb` MongoDB container
- `basketdb` Redis container
- `discountdb` PostgreSQL container

The solution is intended to demonstrate a modular architecture with separate API, application, domain/core, and infrastructure layers.

## Architecture

The codebase follows Clean Architecture best practices:

- `*.API` projects expose HTTP/gRPC endpoints
- `*.Application` projects contain use cases, commands, queries, and DTOs
- `*.Core` projects define domain entities and repository contracts
- `*.Infrastructure` projects implement persistence, settings, and external dependencies

## Prerequisites

- .NET 10 SDK
- Docker
- Docker Compose v2+ (`docker compose`)

## Running locally with Docker

Start the full environment with:

```bash
docker compose up -d
```

Stop the services:

```bash
docker compose stop
```

Stop and remove containers, networks and volumes created by Compose:

```bash
docker compose down
```

Rebuild only the Discount service image:

```bash
docker compose build discount
```

View logs for a service:

```bash
docker logs --tail 100 clean-architecture-dotnet-10-discount-1
```

## Database volume configuration

The PostgreSQL service is configured to mount the volume at `/var/lib/postgresql` to support PostgreSQL 18+ data layout. This is the recommended setup for Docker images using version-specific cluster directories.

## Configuration

- `Services/Discount/Discount.API/appsettings.json` contains default database settings for local development
- `docker-compose.override.yml` contains container environment variables and volume mappings

## Tests

Run the solution tests with:

```bash
dotnet test Ecommerce.slnx --no-restore
```

## GitHub Actions workflows

This repository includes automated workflows for branch-based PR creation:

- `feature-branch-pr.yml`
  - runs when a branch starting with `feature` is pushed
  - restores and tests `Ecommerce.slnx`
  - creates or reuses a pull request into `develop` after successful tests

- `develop-to-master-pr.yml`
  - runs when a pull request targeting `develop` is merged
  - creates or reuses a pull request from `develop` to `master`

## GitHub setup notes

If your repository does not allow GitHub Actions to create PRs with the default token, add a repository secret named `PR_TOKEN` with a Personal Access Token that has repository write permissions.

### Recommended branch protection

Protect `develop` and `master` using GitHub branch protection rules:

- require pull requests before merging
- require status checks to pass
- prevent force pushes and deletion
- restrict pushes to authorized users

## Project documentation

Documentation is available in the `docs/` folder:

- [Setup and Local Execution](docs/setup.md)
- [Architecture](docs/architecture.md)
- [GitHub Actions Workflows](docs/workflows.md)
- [Contributing](docs/contributing.md)

## Contributing

If you want to extend the sample, follow these steps:

1. create a feature branch named `feature/<name>`
2. push changes
3. let the feature workflow run tests and open a PR to `develop`
4. merge into `develop`
5. let the release workflow open a PR from `develop` to `master`

## Notes

- The Discount service Docker image installs `libgssapi-krb5-2` to satisfy Npgsql requirements and avoid runtime warnings.
- The repository uses a clean architecture pattern with separate domain, application, and infrastructure layers for each bounded context.
  