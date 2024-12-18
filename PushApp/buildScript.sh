# 코어 빌드를 하여 dll을 갱신 받는다
cd ../CoreLib/
dotnet build CoreLibrary.csproj


cd ../PushApp/

# 도커 빌드를 하여 이미지 생성
docker build -t pushapp:latest -f Dockerfile ..

# 도커 hub로 push 하기전 올바른 경로 형태로 빌드된 이미지에 대하여 태깅
docker tag pushapp:latest rumdice/push-app:latest 

# 도커 hub 로그인 - credential 처리가 작업자 pc에 되어 있어야 함
docker login

# 지정된 docker hub으로 이미지 푸쉬
docker push rumdice/push-app:latest 

# 쿠버네티스 배포 강제 적용
kubectl rollout restart deployment/push-app-deployment
kubectl apply -f push-app.yaml      

# docker hub에 올라갔으니 로컬에 생성된 docker image 정리하기
docker rmi pushapp
docker rmi rumdice/push-app