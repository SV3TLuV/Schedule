FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5291

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Schedule.Api/Schedule.Api.csproj", "Schedule.Api/"]
COPY ["Schedule.Application/Schedule.Application.csproj", "Schedule.Application/"]
COPY ["Schedule.Persistence/Schedule.Persistence.csproj", "Schedule.Persistence/"]
COPY ["Schedule.Core/Schedule.Core.csproj", "Schedule.Core/"]
RUN dotnet restore "Schedule.Api/Schedule.Api.csproj"
COPY . .
WORKDIR "/src/Schedule.Api"
RUN dotnet build "Schedule.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Schedule.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Schedule.Api.dll"]
