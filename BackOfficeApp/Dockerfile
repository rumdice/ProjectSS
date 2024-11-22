# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 실행 포트 지정
EXPOSE 7188
ENV ASPNETCORE_URLS=http://+:7188

# 프로젝트 파일 복사 및 복구
COPY ["BackOfficeApp/BackOfficeApp.csproj", "BackOfficeApp/"]

# dll 복사 (같은 경로에 있는 것으로 사용)
COPY ["BackOfficeApp/Dll/CoreLibrary.dll", "BackOfficeApp/Dll/"]

RUN dotnet restore "BackOfficeApp/BackOfficeApp.csproj"

# 모든 소스 파일 복사
COPY . .

# 프로젝트 빌드
WORKDIR /src/BackOfficeApp
RUN dotnet build "BackOfficeApp.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "BackOfficeApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 컨테이너 시작 명령
ENTRYPOINT ["dotnet", "BackOfficeApp.dll"]
