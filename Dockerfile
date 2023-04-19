FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DepositService/DepositService.csproj", "DepositService/"]
RUN dotnet restore "DepositService/DepositService.csproj"
COPY . .
WORKDIR "/src/DepositService"
RUN dotnet build "DepositService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DepositService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DepositService.dll"]
