# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 실행 포트 지정
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# 프로젝트 파일 복사 및 복구
COPY ["WebApp/WebApp.csproj", "WebApp/"]

#COPY ["CoreSystem/CoreLibrary/bin/Debug/net8.0/CoreLibrary.dll", "WebApp/bin/Debug/net8.0/"]
# dll 복사 (같은 경로에 있는 것으로 사용)
COPY ["WebApp/Dll/CoreLibrary.dll", "WebApp/Dll/"]

RUN dotnet restore "WebApp/WebApp.csproj"

# 모든 소스 파일 복사
COPY . .

# 프로젝트 빌드
WORKDIR /src/WebApp
RUN dotnet build "WebApp.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 컨테이너 시작 명령
ENTRYPOINT ["dotnet", "WebApp.dll"]
