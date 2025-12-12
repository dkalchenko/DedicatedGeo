FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src
COPY build.targets .
COPY ["DedicatedGeo.Mono.Api/DedicatedGeo.Mono.Api.csproj", "DedicatedGeo.Mono.Api/"]
COPY ["DedicatedGeo.Mono.Bootstrap/DedicatedGeo.Mono.Bootstrap.csproj", "DedicatedGeo.Mono.Bootstrap/"]
COPY ["DedicatedGeo.Mono.Dtos/DedicatedGeo.Mono.Dtos.csproj", "DedicatedGeo.Mono.Dtos/"]
COPY ["DedicatedGeo.Mono.Core/DedicatedGeo.Mono.Core.csproj", "DedicatedGeo.Mono.Core/"]
COPY ["DedicatedGeo.Mono.Core.Abstractions/DedicatedGeo.Mono.Core.Abstractions.csproj", "DedicatedGeo.Mono.Core.Abstractions/"]
COPY ["DedicatedGeo.Mono.Common/DedicatedGeo.Mono.Common.csproj", "DedicatedGeo.Mono.Common/"]
COPY ["DedicatedGeo.Mono.Dal/DedicatedGeo.Mono.Dal.csproj", "DedicatedGeo.Mono.Dal/"]
COPY ["DedicatedGeo.Mono.Dal.Abstractions/DedicatedGeo.Mono.Dal.Abstractions.csproj", "DedicatedGeo.Mono.Dal.Abstractions/"]
COPY ["DedicatedGeo.Mono.Models/DedicatedGeo.Mono.Models.csproj", "DedicatedGeo.Mono.Models/"]
RUN dotnet restore "DedicatedGeo.Mono.Api/DedicatedGeo.Mono.Api.csproj"
COPY . .
WORKDIR "/src/DedicatedGeo.Mono.Api"
RUN dotnet build "DedicatedGeo.Mono.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DedicatedGeo.Mono.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /Database
ENTRYPOINT ["dotnet", "DedicatedGeo.Mono.Api.dll"]
