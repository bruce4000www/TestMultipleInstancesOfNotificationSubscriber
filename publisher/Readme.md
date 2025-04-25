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
2. Execute following command to generate the image:
```cmd
docker buildx build -f Dockerfile -t notification/publisher --build-arg BUILD_BASE_VERSION=8.0-alpine-synapxe.1.15 --secret id=nugetconfig,src=nuget.local.config .
```
