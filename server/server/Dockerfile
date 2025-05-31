FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# העתקים לפי הנתיב האמיתי
COPY ["server/server/server.csproj", "server/server/"]
COPY ["server/server.core/server.core.csproj", "server/server.core/"]
COPY ["server/server.data/server.data.csproj", "server/server.data/"]
COPY ["server/server.service/server.service.csproj", "server/server.service/"]

RUN dotnet restore "server/server/server.csproj"

COPY . .

WORKDIR "/src/server/server"
RUN dotnet build "server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "server.dll"]
