FROM microsoft/aspnetcore-build:2.0 AS builder
WORKDIR /source
COPY Web/*.csproj .
RUN dotnet restore
COPY Web .
RUN dotnet publish --output /app --configuration Release

FROM microsoft/aspnetcore:2.0
RUN apt-get update && apt-get dist-upgrade -y && apt-get install -y sqlite3 libsqlite3-dev
WORKDIR /app
COPY --from=builder /app .

ENTRYPOINT ["dotnet", "Web.dll"]
