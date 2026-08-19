# ProdRoad

Production management application for a manufacturing workflow: customers and their agreed prices, items and the processes each item passes through, roadmaps tying processes into a route, orders and order rows, and the teams assigned to the work.

The repository holds three generations of the application, each a full rebuild rather than a continuation, so the same domain is solved three times with a different architecture each time. All three lines are present in the history of `main`.

## Generations

### 2021 — ProdManagerApp

```
Domain.Base / Domain.App          entities
Contracts.Domain.Base
DAL.Base / DAL.Base.EF            repository and unit of work base
DAL.App.EF                        application data access
Contracts.DAL.Base / .App         data access interfaces
DTO.App
Extensions.Base
WebApp                            MVC web layer
```

Two layers: data access and web. Controllers talk to repositories through the unit of work, with no service layer in between.

### 2023 — ProdRoad

```
Domain.Base / Domain.App
DAL.Base / DAL.Base.EF / DAL.App.EF
DAL.App.Contracts / DAL.App.DTO.App
BLL.Base / BLL.App                business logic
BLL.App.Contracts / BLL.App.DTO
Base.Contracts / .Domain / .DAL / .BLL
Public.App.Contracts              public API contracts
WebApp
```

A business logic layer is introduced between web and data access, each layer with its own DTOs and contracts, so the web layer no longer reaches into persistence. Contracts are split per layer and a public API surface is defined separately.

The front end for this generation is a separate JavaScript client, on the `ui-2023` branch.

### 2024 — ProdRoad, current

```
ProdRoad/
  App.Domain                      entities, including Identity
  App.DAL.EF
  App.Contracts.DAL
  Base.Domain / Base.DAL.EF
  Base.Contracts.Domain / .DAL
  Helpers
  WebApp                          MVC views plus ApiControllers
ProdRoadUI/prod-app               Next.js front end
Docs/                             specification
Scripts/
```

Naming moves to the `App.*` / `Base.*` convention, the API controllers live alongside the MVC views in the web project, and the UI is a Next.js application in the same repository. Runs on .NET 8, with `docker-compose.yml` for the database.

## Branches

| branch | contents |
|---|---|
| `main` | the 2024 application; history covers all three generations |
| `gen1-2021` | the 2021 ProdManagerApp line on its own |
| `gen2-2023` | the 2023 ProdRoad line on its own |
| `ui-2023` | the JavaScript front end built against the 2023 backend |

## Domain

`Customer`, `CustomerPrice`, `Price`, `Address`, `Item`, `Process`, `ItemProcess`, `RoadMap`, `Order`, `OrderRow`, `Team`, `UserTeam`

## Stack

.NET 8, ASP.NET Core MVC and Web API, Entity Framework Core, ASP.NET Core Identity, Next.js 14, React 18, TypeScript, Docker Compose

## Running

```
cd ProdRoad
dotnet ef database update --project App.DAL.EF --startup-project WebApp
dotnet run --project WebApp
```

```
cd ProdRoadUI/prod-app
npm install
npm run dev
```
