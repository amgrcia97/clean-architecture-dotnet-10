# Architecture

This repository follows Clean Architecture principles with a separate layer for API, application, core/domain, and infrastructure.

## Services

- `Catalog`: handles product catalog data and queries.
- `Basket`: manages shopping basket state and caching.
- `Discount`: applies discounts and stores coupon data.

## Layered structure

Each service is split into four projects:

- `*.API`: exposes endpoints and configures application services.
- `*.Application`: contains use cases, commands, queries, DTOs, and handlers.
- `*.Core`: defines entities, business rules, and repository contracts.
- `*.Infrastructure`: provides persistence, settings, and external implementation details.

## Benefits

- clear separation of concerns
- easier testing and maintenance
- each service can evolve independently
- dependency rule: outer layers depend on inner layers only
