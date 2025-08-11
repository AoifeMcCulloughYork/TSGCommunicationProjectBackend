FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all project files at once
COPY *.sln ./
COPY TSGCommunicationProjectBackend.Common/*.csproj TSGCommunicationProjectBackend.Common/
COPY TSGCommunicationProjectBackend.Data/*.csproj TSGCommunicationProjectBackend.Data/
COPY TSGCommunicationProjectBackend.Service/*.csproj TSGCommunicationProjectBackend.Service/
COPY TSGCommunicationProjectBackend.Api/*.csproj TSGCommunicationProjectBackend.Api/

# Restore from solution level
RUN dotnet restore

# Copy source and build
COPY . .
RUN dotnet build -c Release 

FROM build AS publish
RUN dotnet publish TSGCommunicationProjectBackend.Api/TSGCommunicationProjectBackend.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TSGCommunicationProjectBackend.Api.dll"]