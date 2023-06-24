#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OCA.API/OCA.API.csproj", "OCA.API/"]
RUN dotnet restore "OCA.API/OCA.API.csproj"
COPY . .
WORKDIR "/src/OCA.API"
RUN dotnet build "OCA.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN \ 
  apt update && \
  apt install -y ca-certificates && \
  update-ca-certificates
RUN dotnet publish "OCA.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OCA.API.dll"]