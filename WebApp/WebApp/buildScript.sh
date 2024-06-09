# 원버튼 빌드를 위하여 이곳에서 카피 쉘 스크립트 수행
sh copyDll.sh

# 도커 빌드를 하여 이미지 생성
docker build -t webapp:latest -f Dockerfile ..  

# 도커 hub로 push 하기전 올바른 경로 형태로 빌드된 이미지에 대하여 태깅
docker tag webapp:latest rumdice/web-app:latest 

# 도커 hub 로그인 - credential 처리가 작업자 pc에 되어 있어야 함?
docker login

# 지정된 docker hub으로 이미지 푸쉬
docker push rumdice/web-app:latest 

# docker hub에 올라갔으니 로컬에 생성된 docker image 정리하기
docker rmi webapp
docker rmi rumdice/web-app