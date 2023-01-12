FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MinimalApi.NoControllers/MinimalApi.NoControllers.csproj", "MinimalApi.NoControllers/"]
RUN dotnet restore "MinimalApi.NoControllers/MinimalApi.NoControllers.csproj"
COPY . .
WORKDIR "/src/MinimalApi.NoControllers"
RUN dotnet build "MinimalApi.NoControllers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalApi.NoControllers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalApi.NoControllers.dll"]
