개발 사항
Blazor 기반의 운영툴을 제작
Server 로 제작 (JS코드의 최소화)

--------------------------------------------------------------


2024.05.04

프로젝트 생성하기
dotnet new blazor -o AdminApp

빌드하기
dotnet build 

https에서 실행 가능하도록 하기
dotnet dev-cetrs https --trust

배포하기
dotnet publish -c Release

확인하기
ls bin/Release/net8.0/publish



2024.05.14
쿠버네티스 배포 환경 추가하기 (DockerFile)

도커 이미지 파일 만들기
방법 1
.csproj 파일이 있는 폴더 위치로 이동하여
vi Dockerfile


방법 2
vscode Docker Extention 설치
cmd + shift + p
도커 파일 생성 : Docker: Add Docker Files to workspace
플랫폼 선택 : .NET: ASP.NET Core
OS 선택 : Linux
포트 번호 입럭 : 5114
Docker Compose 파일은 일단 미포함 : No


도커 빌드 
docker build -f Dockerfile ..


2024.06.11
코어 시스템 적용 하기
- DockerFile 수정 필요
- yaml 스크립트 수정 필요
쿠버네티스에 배포하기


2024.06.15
해당 프로젝트 웹어셈블리로 교체하기
dotnet new blazorwasm -o BackOfficeApp
cd BackOfficeApp
dotnet run

entryPoint 개선 적용 해보기 startup.cs 사용.