#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChTi.Web.Api/ChTi.Web.Api.csproj", "ChTi.Web.Api/"]
RUN dotnet restore "ChTi.Web.Api/ChTi.Web.Api.csproj"
COPY . .
WORKDIR "/src/ChTi.Web.Api"
RUN dotnet build "ChTi.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChTi.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChTi.Web.Api.dll"]