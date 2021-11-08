# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Install dotnet tools
COPY .config ./
RUN dotnet tool restore

# Install NuGet packages
COPY *.csproj ./
RUN dotnet restore

# Install libman libraries
COPY libman.json ./
RUN dotnet libman restore

# Build project
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

# Copy app files
COPY --from=build-env /app/out .

EXPOSE 5000
# TODO: Add dotnet ef database update
CMD [ "dotnet", "adnumaZ.dll" ]
