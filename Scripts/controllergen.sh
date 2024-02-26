#!/bin/sh
cd ../ProdRoad/ProdRoad
dotnet aspnet-codegenerator controller -name AddressController -actions -m Address -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CustomerController -actions -m Customer -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CustomerPriceController -actions -m CustomerPrice -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemController -actions -m Item -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemProcessController -actions -m ItemProcess -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PriceController -actions -m Price -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProcessController -actions -m Process -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RoadMapController -actions -m RoadMap -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TeamController -actions -m Team -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserTeamController -actions -m UserTeam -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderController -actions -m Order -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OrderRowController -actions -m OrderRow -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f