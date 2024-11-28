#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NadinSoft.API/NadinSoft.API.csproj", "NadinSoft.API/"]
COPY ["NadinfSoft.Identity/NadinfSoft.Identity.csproj", "NadinfSoft.Identity/"]
COPY ["NadinSoft.Application/NadinSoft.Application.csproj", "NadinSoft.Application/"]
COPY ["NadinSoft.Domain/NadinSoft.Domain.csproj", "NadinSoft.Domain/"]
COPY ["NadinSoft.Persistence/NadinSoft.Persistence.csproj", "NadinSoft.Persistence/"]
COPY ["NadinSoft.Presentation/NadinSoft.Presentation.csproj", "NadinSoft.Presentation/"]
RUN dotnet restore "./NadinSoft.API/./NadinSoft.API.csproj"
COPY . .
WORKDIR "/src/NadinSoft.API"
RUN dotnet build "./NadinSoft.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NadinSoft.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NadinSoft.API.dll"]