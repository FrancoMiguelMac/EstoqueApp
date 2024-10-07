FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Estoque.API/Estoque.API.csproj", "Estoque.API/"]
COPY ["Estoque.Application/Estoque.Application.csproj", "Estoque.Application/"]
COPY ["Estoque.Crosscutting/Estoque.Crosscutting.csproj", "Estoque.Crosscutting/"]
COPY ["Estoque.Domain/Estoque.Domain.csproj", "Estoque.Domain/"]
COPY ["Estoque.Domain.Core/Estoque.Domain.Core.csproj", "Estoque.Domain.Core/"]
COPY ["Estoque.Infra/Estoque.Infra.csproj", "Estoque.Infra/"]
RUN dotnet restore "./Estoque.API/Estoque.API.csproj"
COPY . .
WORKDIR "/src/Estoque.API"
RUN dotnet build "./Estoque.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Estoque.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Estoque.API.dll"]