FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RandomComicApi/RandomComicApi.csproj", "RandomComicApi/"]
RUN dotnet restore "RandomComicApi/RandomComicApi.csproj"
COPY . .
WORKDIR "/src/RandomComicApi"
RUN dotnet build "RandomComicApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RandomComicApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RandomComicApi.dll"]
