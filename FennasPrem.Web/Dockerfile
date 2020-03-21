#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging. 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

ENV ASPNETCORE_ENVIRONMENT=Development 

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FennasPrem.Web.csproj", "/"]
RUN dotnet restore "/FennasPrem.Web.csproj"
COPY . .
RUN dotnet build "FennasPrem.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FennasPrem.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FennasPrem.Web.dll"]