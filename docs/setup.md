# Setup and Local Execution

## Prerequisites

- .NET 10 SDK installed
- Docker installed
- Docker Compose v2+ (`docker compose`)

## Running locally

Start the full environment:

```bash
docker compose up -d
```

Stop the containers:

```bash
docker compose stop
```

Stop and remove the environment created by Compose:

```bash
docker compose down
```

Rebuild only the Discount service image:

```bash
docker compose build discount
```

View logs for the Discount service:

```bash
docker logs --tail 100 clean-architecture-dotnet-10-discount-1
```

## PostgreSQL volume configuration

The `discountdb` service mounts a Docker volume at `/var/lib/postgresql` to support PostgreSQL 18+ data layout. This is the recommended setup for Docker images using version-specific cluster directories.

## Configuration files

- `docker-compose.yml` defines the services and shared volumes.
- `docker-compose.override.yml` defines container environment variables, ports, and volume mappings for local development.
- `Services/Discount/Discount.API/appsettings.json` contains default connection settings for the Discount service.
