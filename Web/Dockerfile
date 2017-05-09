FROM microsoft/aspnetcore:1.1.2
RUN apt-get update && apt-get dist-upgrade -y && apt-get install -y sqlite3 libsqlite3-dev
RUN mkdir -p /app
WORKDIR /app
COPY bin/Release/netcoreapp1.1/publish /app

ENTRYPOINT ["dotnet", "Web.dll"]
