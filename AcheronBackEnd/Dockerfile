# build PLEASE WORKPLEASE WORKPLEASE WORK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["AcheronBackEnd.csproj", "./"]
RUN dotnet restore "AcheronBackEnd.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AcheronBackEnd.dll"]
