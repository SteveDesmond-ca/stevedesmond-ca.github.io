FROM microsoft/aspnetcore:2.0.0
RUN apt-get update && apt-get dist-upgrade -y && apt-get install -y sqlite3 libsqlite3-dev
RUN mkdir -p /app
WORKDIR /app
COPY bin/Release/netcoreapp2.0/publish /app

ENTRYPOINT ["dotnet", "Web.dll"]
