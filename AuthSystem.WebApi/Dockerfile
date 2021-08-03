#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AuthSystem.WebApi/AuthSystem.WebApi.csproj", "AuthSystem.WebApi/"]
RUN dotnet restore "AuthSystem.WebApi/AuthSystem.WebApi.csproj"
COPY . .
WORKDIR "/src/AuthSystem.WebApi"
RUN dotnet build "AuthSystem.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthSystem.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthSystem.WebApi.dll"]