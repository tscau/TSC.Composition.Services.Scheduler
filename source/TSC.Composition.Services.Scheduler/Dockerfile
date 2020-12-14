#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 30100
EXPOSE 1433 
#Install packages
RUN apt-get -y update
RUN apt-get -y install net-tools
RUN apt-get -y install iputils-ping

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TSC.Composition.Services.Scheduler.csproj", "TSC.Composition.Services.Scheduler/"]
RUN dotnet restore "TSC.Composition.Services.Scheduler/TSC.Composition.Services.Scheduler.csproj"
COPY . .
WORKDIR "/src/TSC.Composition.Services.Scheduler"
RUN dotnet build "TSC.Composition.Services.Scheduler.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "TSC.Composition.Services.Scheduler.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TSC.Composition.Services.Scheduler.dll"]