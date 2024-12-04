# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 실행 포트 지정
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production


COPY LogApp/LogApp.csproj ./LogApp/
COPY LogApp/Dll/CoreLibrary.dll ./LogApp/Dll/


RUN dotnet restore "LogApp/LogApp.csproj"

# 모든 소스 파일 복사
COPY . .

# 프로젝트 빌드
WORKDIR /src/LogApp
RUN dotnet build "LogApp.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "LogApp.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 컨테이너 시작 명령
ENTRYPOINT ["dotnet", "LogApp.dll"]
