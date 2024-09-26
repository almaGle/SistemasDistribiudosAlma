FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:80

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Chatp2p.csproj", "./"]
RUN dotnet restore "Chatp2p.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Chatp2p.csproj" -c $configuration -o /app/build 

FROM build AS publish 
ARG configuration=Release
RUN dotnet publish "Chatp2p.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Chatp2p.dll"]