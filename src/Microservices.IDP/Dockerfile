#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices.IDP/Microservices.IDP.csproj", "Microservices.IDP/"]
COPY ["Microservices.IDP.Presentation/Microservices.IDP.Presentation.csproj", "Microservices.IDP.Presentation/"]
COPY ["Microservices.IDP.Infrastructure/Microservices.IDP.Infrastructure.csproj", "Microservices.IDP.Infrastructure/"]
RUN dotnet restore "Microservices.IDP/Microservices.IDP.csproj"
COPY . .
WORKDIR "/src/Microservices.IDP"
RUN dotnet build "Microservices.IDP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.IDP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.IDP.dll"]