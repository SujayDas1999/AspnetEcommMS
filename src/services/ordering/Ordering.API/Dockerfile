#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/ordering/Ordering.API/Ordering.API.csproj", "services/ordering/Ordering.API/"]
COPY ["services/ordering/Ordering.Application/Ordering.Application.csproj", "services/ordering/Ordering.Application/"]
COPY ["services/ordering/Ordering.Domain/Ordering.Domain.csproj", "services/ordering/Ordering.Domain/"]
COPY ["services/ordering/Ordering.Infra/Ordering.Infra.csproj", "services/ordering/Ordering.Infra/"]
RUN dotnet restore "services/ordering/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/services/ordering/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]