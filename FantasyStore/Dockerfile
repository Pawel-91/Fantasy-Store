#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
#ENV ASPNETCORE_URLS http://+:5000
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["./FantasyStore.csproj", "FantasyStore/"]
RUN dotnet restore "FantasyStore/FantasyStore.csproj"
COPY . "./FantasyStore"
WORKDIR "/src/FantasyStore"
RUN dotnet build "FantasyStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FantasyStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "FantasyStore.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FantasyStore.dll