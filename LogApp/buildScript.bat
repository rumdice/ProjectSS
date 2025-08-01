@echo off

:: 1. CoreLibrary 빌드
cd ../CoreLib/
dotnet build CoreLibrary.csproj
if errorlevel 1 (
    echo CoreLibrary 빌드 실패. 스크립트를 종료합니다.
    exit /b 1
)

:: 2. LogApp 디렉토리로 이동
cd ../LogApp/

:: 3. Docker 빌드 및 태그 생성
docker buildx create --name multiarch-builder --use >nul 2>&1
docker run --rm --privileged multiarch/qemu-user-static --reset -p yes

docker buildx build --platform linux/amd64,linux/arm64 -t logapp:latest -f Dockerfile .. --push
if errorlevel 1 (
    echo Docker 이미지 빌드 실패. 스크립트를 종료합니다.
    exit /b 1
)

:: 4. Docker Hub 로그인
docker login
if errorlevel 1 (
    echo Docker Hub 로그인 실패. 스크립트를 종료합니다.
    exit /b 1
)

:: 5. Docker 이미지 태깅 및 푸쉬
docker tag logapp:latest rumdice/log-app:latest
docker push rumdice/log-app:latest
if errorlevel 1 (
    echo Docker 이미지 푸쉬 실패. 스크립트를 종료합니다.
    exit /b 1
)

:: 6. 쿠버네티스 배포 (필요 시 주석 해제)
:: kubectl rollout restart deployment/log-app-deployment
:: kubectl apply -f log-app.yaml

:: 7. 로컬 Docker 이미지 정리
docker rmi logapp >nul 2>&1
docker rmi rumdice/log-app >nul 2>&1

echo 빌드 및 배포 완료!
