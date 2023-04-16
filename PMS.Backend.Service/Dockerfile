#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PMS.Backend.Service/PMS.Backend.Service.csproj", "PMS.Backend.Service/"]
COPY ["PMS.Backend.Core/PMS.Backend.Core.csproj", "PMS.Backend.Core/"]
COPY ["PMS.Backend.Common/PMS.Backend.Common.csproj", "PMS.Backend.Common/"]
COPY ["PMS.Backend.Features/PMS.Backend.Features.csproj", "PMS.Backend.Features/"]
RUN dotnet restore "PMS.Backend.Service/PMS.Backend.Service.csproj"
COPY . .
WORKDIR "/src/PMS.Backend.Service"
RUN dotnet build "PMS.Backend.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PMS.Backend.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PMS.Backend.Service.dll"]