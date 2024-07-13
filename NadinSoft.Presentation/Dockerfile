#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NadinSoft.Presentation/NadinSoft.Presentation.csproj", "NadinSoft.Presentation/"]
COPY ["NadinSoft.Domain/NadinSoft.Domain.csproj", "NadinSoft.Domain/"]
COPY ["NadinSoft.Infrastructure/NadinSoft.Infrastructure.csproj", "NadinSoft.Infrastructure/"]
COPY ["NadinSoft.Application/NadinSoft.Application.csproj", "NadinSoft.Application/"]
RUN dotnet restore "./NadinSoft.Presentation/NadinSoft.Presentation.csproj"
COPY . .
WORKDIR "/src/NadinSoft.Presentation"
RUN dotnet build "./NadinSoft.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NadinSoft.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NadinSoft.Presentation.dll"]