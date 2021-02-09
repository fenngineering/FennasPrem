FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /app

COPY *.sln .
COPY Commentator.OrchardCore/*.csproj ./Commentator.OrchardCore/
COPY FennasPrem.Web/*.csproj ./FennasPrem.Web/

ADD LICENSE LICENSE

# Copy csproj and restore as distinct layers
RUN dotnet restore --disable-parallel

# Copy everything else and build
COPY Commentator.OrchardCore/. ./Commentator.OrchardCore/
COPY FennasPrem.Web/. ./FennasPrem.Web/

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS http://+:80

ENV ASPNETCORE_ENVIRONMENT=Production 
EXPOSE 80

ENTRYPOINT ["dotnet", "FennasPrem.Web.dll"]