FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DailyDiary.API/DailyDiary.API.csproj", "DailyDiary.API/"]
RUN dotnet restore "DailyDiary.API/DailyDiary.API.csproj"
COPY . .
WORKDIR "/src/DailyDiary.API"
RUN dotnet build "DailyDiary.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DailyDiary.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DailyDiary.API.dll"]
