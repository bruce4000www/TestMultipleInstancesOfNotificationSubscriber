ARG BUILD_BASE_VERSION

FROM packages.hip.synapxe.sg/dotnet/sdk:${BUILD_BASE_VERSION} AS build
WORKDIR /build
COPY . .
ARG NUGET_CERT_REVOCATION_MODE=offline
RUN --mount=type=secret,id=nugetconfig dotnet restore --configfile /run/secrets/nugetconfig

FROM build AS publish
RUN dotnet publish NotificationPublisher.csproj -c Release -o /app --no-restore
RUN unzip  /app/R4/specification.zip -d /app/R4/specification

FROM packages.hip.synapxe.sg/dotnet/aspnet:${BUILD_BASE_VERSION} as final
WORKDIR /app
COPY --from=publish /app .
COPY Cert /app

ENV ASPNETCORE_ENVIRONMENT=container
ENV Logging__Console__FormatterName=Simple
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 8080

ENTRYPOINT ["sh", "-c", "ulimit -Sn 4096 && ./NotificationPublisher"]
