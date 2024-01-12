#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# copy all the layers' cyproj files into their respective folders
COPY ["Acme.Application/Acme.Application.csproj", "Acme.Application/"]
COPY ["Acme.Core/Acme.Core.csproj", "Acme.Core/"]
COPY ["Acme.Infrastructure.EF.PostgreSql/Acme.Infrastructure.EF.PostgreSql.csproj", "Acme.Infrastructure.EF.PostgreSql/"]
COPY ["Acme.Interface.WebAPI/Acme.Interface.WebAPI.csproj", "Acme.Interface.WebAPI/"]
COPY ["Enterwell.Exceptions.Standard/Enterwell.Exceptions.Standard.csproj", "Enterwell.Exceptions.Standard/"]
COPY ["Enterwell.Exceptions.Standard.Web/Enterwell.Exceptions.Standard.Web.csproj", "Enterwell.Exceptions.Standard.Web/"]

# run restore over the WebAPI.Starter project - this pulls restore over all other dependent projects as well
RUN dotnet restore "./Acme.Interface.WebAPI/./Acme.Interface.WebAPI.csproj"
COPY . .
WORKDIR "/src/Acme.Interface.WebAPI"
RUN dotnet build "./Acme.Interface.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Acme.Interface.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acme.Interface.WebAPI.dll"]