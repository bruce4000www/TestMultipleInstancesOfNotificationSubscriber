## Build Image
1. Create `nuget.local.config` file in the current directory with the following content:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="nexus" value="https://packages.hip.synapxe.sg/repository/nuget/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <nexus>
        <add key="Username" value="<YOUR_USER_NAME>" />
        <add key="cleartextpassword" value="<YOUR_PASSWORD>" />
      </nexus>
  </packageSourceCredentials>
</configuration>
```
Note: replace `<YOUR_USER_NAME>` and `<YOUR_PASSWORD>` with actual value.

2. Execute following command to generate the image:
```cmd
docker buildx build -f Dockerfile -t notification/subscriber --build-arg BUILD_BASE_VERSION=8.0-alpine-synapxe.1.15 --secret id=nugetconfig,src=nuget.local.config .
```
3. Create a docker compose file with following content:
```yaml
name: notification
services:
  database:
    image: 'mcr.microsoft.com/mssql/server:2022-latest'
    container_name: notification_db
    environment:
      ACCEPT_EULA: Y
      MSSQL_SA_PASSWORD: Ngsc082itjhqWcqQ10Dp
    ports:
      - '6433:1433'
    healthcheck:
      test: '/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Ngsc082itjhqWcqQ10Dp -C -Q "Select 1" || exit 1'
  publisher:
    image: 'notification/publisher'
    container_name: notification_publisher
    environment:
      ASPNETCORE_URLS: 'https://*:8080'
      ASPNETCORE_ENVIRONMENT: container
      ASPNETCORE_Kestrel__Certificates__Default__Password: password
      ASPNETCORE_Kestrel__Certificates__Default__Path: /app/integrationtest.pfx
      AWS_ACCESS_KEY_ID: YOUR_AWS_KEY_ID
      AWS_SECRET_ACCESS_KEY: YOUR_AWS_KEY_VALUE
    ports:
      - '9901:8080'
    healthcheck:
      test: 'curl https://localhost:8080/health -k || exit 1'
    depends_on:
      database:
        condition: service_healthy
    extra_hosts:
      - "host.docker.internal:172.17.0.1"
  subscriber1:
    image: 'notification/subscriber'
    container_name: notification_subscriber1
    environment:
      ASPNETCORE_URLS: 'https://*:8080'
      ASPNETCORE_ENVIRONMENT: container
      ASPNETCORE_Kestrel__Certificates__Default__Password: password
      ASPNETCORE_Kestrel__Certificates__Default__Path: /app/integrationtest.pfx
      AWS_ACCESS_KEY_ID: YOUR_AWS_KEY_ID
      AWS_SECRET_ACCESS_KEY: YOUR_AWS_KEY_VALUE
    ports:
      - '9902:8080'
    healthcheck:
      test: 'curl https://localhost:8080/health -k || exit 1'
    depends_on:
      publisher:
        condition: service_healthy
    extra_hosts:
      - "host.docker.internal:172.17.0.1"
  subscriber2:
    image: 'notification/subscriber'
    container_name: notification_subscriber2
    environment:
      ASPNETCORE_URLS: 'https://*:8080'
      ASPNETCORE_ENVIRONMENT: container
      ASPNETCORE_Kestrel__Certificates__Default__Password: password
      ASPNETCORE_Kestrel__Certificates__Default__Path: /app/integrationtest.pfx
      AWS_ACCESS_KEY_ID: YOUR_AWS_KEY_ID
      AWS_SECRET_ACCESS_KEY: YOUR_AWS_KEY_VALUE
    ports:
      - '9903:8080'
    healthcheck:
      test: 'curl https://localhost:8080/health -k || exit 1'
    depends_on:
      publisher:
        condition: service_healthy
    extra_hosts:
      - "host.docker.internal:172.17.0.1"
  subscriber3:
    image: 'notification/subscriber'
    container_name: notification_subscriber3
    environment:
      ASPNETCORE_URLS: 'https://*:8080'
      ASPNETCORE_ENVIRONMENT: container
      ASPNETCORE_Kestrel__Certificates__Default__Password: password
      ASPNETCORE_Kestrel__Certificates__Default__Path: /app/integrationtest.pfx
      AWS_ACCESS_KEY_ID: YOUR_AWS_KEY_ID
      AWS_SECRET_ACCESS_KEY: YOUR_AWS_KEY_VALUE
    ports:
      - '9904:8080'
    healthcheck:
      test: 'curl https://localhost:8080/health -k || exit 1'
    depends_on:
      publisher:
        condition: service_healthy
    extra_hosts:
      - "host.docker.internal:172.17.0.1"
  subscriber4:
    image: 'notification/subscriber'
    container_name: notification_subscriber4
    environment:
      ASPNETCORE_URLS: 'https://*:8080'
      ASPNETCORE_ENVIRONMENT: container
      ASPNETCORE_Kestrel__Certificates__Default__Password: password
      ASPNETCORE_Kestrel__Certificates__Default__Path: /app/integrationtest.pfx
      AWS_ACCESS_KEY_ID: YOUR_AWS_KEY_ID
      AWS_SECRET_ACCESS_KEY: YOUR_AWS_KEY_VALUE
    ports:
      - '9905:8080'
    healthcheck:
      test: 'curl https://localhost:8080/health -k || exit 1'
    depends_on:
      publisher:
        condition: service_healthy
    extra_hosts:
      - "host.docker.internal:172.17.0.1"
  subscriber5:
    image: 'notification/subscriber'
    container_name: notification_subscriber5
    environment:
      ASPNETCORE_URLS: 'https://*:8080'
      ASPNETCORE_ENVIRONMENT: container
      ASPNETCORE_Kestrel__Certificates__Default__Password: password
      ASPNETCORE_Kestrel__Certificates__Default__Path: /app/integrationtest.pfx
      AWS_ACCESS_KEY_ID: YOUR_AWS_KEY_ID
      AWS_SECRET_ACCESS_KEY: YOUR_AWS_KEY_VALUE
    ports:
      - '9906:8080'
    healthcheck:
      test: 'curl https://localhost:8080/health -k || exit 1'
    depends_on:
      publisher:
        condition: service_healthy
    extra_hosts:
      - "host.docker.internal:172.17.0.1"
  subscriber6:
    image: 'notification/subscriber'
    container_name: notification_subscriber6
    environment:
      ASPNETCORE_URLS: 'https://*:8080'
      ASPNETCORE_ENVIRONMENT: container
      ASPNETCORE_Kestrel__Certificates__Default__Password: password
      ASPNETCORE_Kestrel__Certificates__Default__Path: /app/integrationtest.pfx
      AWS_ACCESS_KEY_ID: YOUR_AWS_KEY_ID
      AWS_SECRET_ACCESS_KEY: YOUR_AWS_KEY_VALUE
    ports:
      - '9907:8080'
    healthcheck:
      test: 'curl https://localhost:8080/health -k || exit 1'
    depends_on:
      publisher:
        condition: service_healthy
    extra_hosts:
      - "host.docker.internal:172.17.0.1"
  subscriber7:
    image: 'notification/subscriber'
    container_name: notification_subscriber7
    environment:
      ASPNETCORE_URLS: 'https://*:8080'
      ASPNETCORE_ENVIRONMENT: container
      ASPNETCORE_Kestrel__Certificates__Default__Password: password
      ASPNETCORE_Kestrel__Certificates__Default__Path: /app/integrationtest.pfx
      AWS_ACCESS_KEY_ID: YOUR_AWS_KEY_ID
      AWS_SECRET_ACCESS_KEY: YOUR_AWS_KEY_VALUE
    ports:
      - '9908:8080'
    healthcheck:
      test: 'curl https://localhost:8080/health -k || exit 1'
    depends_on:
      publisher:
        condition: service_healthy
    extra_hosts:
      - "host.docker.internal:172.17.0.1"
```
Note: replace `YOUR_AWS_KEY_ID` and `YOUR_AWS_KEY_VALUE` with actual value.

4. Execute the docker-compose.yml file to launch the services:
```cmd
docker compose up -d
```
