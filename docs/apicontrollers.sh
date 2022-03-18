#!/bin/sh
cd ../ProdApp/ProdRoad/WebApp
dotnet aspnet-codegenerator controller -name ActiveNotificationController -actions -m Domain.App.ActiveNotification -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name AddressController -actions -m Domain.App.Address -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CustomerController -actions -m Domain.App.Customer -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CustomerPriceGroupController -actions -m Domain.App.CustomerPriceGroup -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemController -actions -m Domain.App.Item -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemProcedureController -actions -m Domain.App.ItemProcedure -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ItemWarehouseController -actions -m Domain.App.ItemWarehouse -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PriceController -actions -m Domain.App.Price -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name PriceGroupController -actions -m Domain.App.PriceGroup -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ProcedureController -actions -m Domain.App.Procedure -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ProcessController -actions -m Domain.App.Process -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name RoadMapController -actions -m Domain.App.RoadMap -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name TeamController -actions -m Domain.App.Team -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name UserNotificationController -actions -m Domain.App.UserNotification -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name UserTeamController -actions -m Domain.App.UserTeam -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name WarehouseController -actions -m Domain.App.Warehouse -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name CustomerPriceController -actions -m Domain.App.CustomerPrice -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f