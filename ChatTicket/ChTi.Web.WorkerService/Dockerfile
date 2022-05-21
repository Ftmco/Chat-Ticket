#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChTi.Web.WorkerService/ChTi.Web.WorkerService.csproj", "ChTi.Web.WorkerService/"]
RUN dotnet restore "ChTi.Web.WorkerService/ChTi.Web.WorkerService.csproj"
COPY . .
WORKDIR "/src/ChTi.Web.WorkerService"
RUN dotnet build "ChTi.Web.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChTi.Web.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChTi.Web.WorkerService.dll"]