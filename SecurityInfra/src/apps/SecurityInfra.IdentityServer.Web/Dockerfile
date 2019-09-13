FROM microsoft/dotnet:2.2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2.103-sdk AS build
WORKDIR /src
COPY ["src/identityserver/SecurityInfra.IdentityServer.Web/SecurityInfra.IdentityServer.Web.csproj", "src/identityserver/SecurityInfra.IdentityServer.Web/"]
COPY ["src/common/SecurityInfra.Common.Uab/SecurityInfra.Common.Uab.csproj", "src/common/SecurityInfra.Common.Uab/"]
COPY ["src/common/SecurityInfra.Common/SecurityInfra.Common.csproj", "src/common/SecurityInfra.Common/"]
COPY ["src/configuration/SecurityInfra.Configuration/SecurityInfra.Configuration.csproj", "src/configuration/SecurityInfra.Configuration/"]
COPY ["src/configuration/SecurityInfra.Configuration.Mongo/SecurityInfra.Configuration.Mongo.csproj", "src/configuration/SecurityInfra.Configuration.Mongo/"]
RUN dotnet restore "src/identityserver/SecurityInfra.IdentityServer.Web/SecurityInfra.IdentityServer.Web.csproj"
COPY . .
WORKDIR "/src/src/identityserver/SecurityInfra.IdentityServer.Web"
RUN dotnet build "SecurityInfra.IdentityServer.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SecurityInfra.IdentityServer.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SecurityInfra.IdentityServer.Web.dll"]