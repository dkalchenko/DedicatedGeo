FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY build.targets .
COPY ["Idealtex.Mono.Api/Idealtex.Mono.Api.csproj", "Idealtex.Mono.Api/"]
RUN dotnet restore "Idealtex.Mono.Api/Idealtex.Mono.Api.csproj"
COPY . .
WORKDIR "/src/Idealtex.Mono.Api"
RUN dotnet build "Idealtex.Mono.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Idealtex.Mono.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Idealtex.Mono.Api.dll"]
