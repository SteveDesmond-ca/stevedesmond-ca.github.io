FROM microsoft/dotnet:2.1-sdk as builder
WORKDIR /source
COPY Web/*.csproj .
RUN dotnet restore
COPY Web .
RUN dotnet publish --output /app --configuration Release

FROM microsoft/dotnet:2.1-aspnetcore-runtime
RUN apt-get update && apt-get dist-upgrade -y && apt-get install -y sqlite3 libsqlite3-dev
WORKDIR /app
COPY --from=builder /app .

ENTRYPOINT ["dotnet", "Web.dll"]
