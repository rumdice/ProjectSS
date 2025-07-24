# Build Stage
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 실행 포트 지정
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# 프로젝트 파일과 의존성 복사
COPY LogApp/LogApp.csproj ./LogApp/
COPY LogApp/Dll/CoreLibrary.dll ./LogApp/Dll/

# 의존성 복원
RUN dotnet restore "LogApp/LogApp.csproj"

# 소스 파일 복사 및 빌드
COPY . .
WORKDIR /src/LogApp
RUN dotnet build "LogApp.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "LogApp.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

# Final Stage
FROM --platform=$TARGETPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# 배포 파일 복사
COPY --from=publish /app/publish .

# 컨테이너 실행 명령
ENTRYPOINT ["dotnet", "LogApp.dll"]
