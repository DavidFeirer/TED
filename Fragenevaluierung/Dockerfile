# Verwenden des ASP.NET Core SDK-Images f�r den Build-Prozess
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Kopieren der Projektdateien in den Container
COPY . .

# Wiederherstellen der NuGet-Pakete und Kompilieren des Projekts
RUN dotnet restore
RUN dotnet build --configuration Release --no-restore

# Ver�ffentlichen des Projekts
RUN dotnet publish --configuration Release --output /app

# Verwenden des ASP.NET Core Runtime-Images f�r die finale Ver�ffentlichung
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Setzen des Port, den der Container �ffentlich freigeben soll
ENV ASPNETCORE_URLS=http://localhost:5001
EXPOSE 5001

# Ausf�hren der Anwendung
ENTRYPOINT ["dotnet", "Fragenevaluierung.dll"]
